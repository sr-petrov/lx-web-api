namespace WebApi.Taxes;

public class TaxCalculator
{
    private readonly TaxBracketsLoader _taxBracketsLoader;

    public TaxCalculator(TaxBracketsLoader taxBracketsLoader)
    {
        _taxBracketsLoader = taxBracketsLoader;
    }

    public SalaryDetails CalculateTaxes(decimal baseSalary)
    {
        // Use the loaders to get tax brackets
        var taxBrackets = _taxBracketsLoader.LoadTaxBrackets();

        // Calculate income tax based on tax brackets
        decimal incomeTax = CalculateIncomeTax(baseSalary, taxBrackets);

        // Calculate Medicare based on rules
        decimal medicareTax = CalculateMedicareTax(baseSalary);

        // Total tax is the sum of income tax and Medicare
        decimal totalTax = Math.Round(incomeTax + medicareTax, 0, MidpointRounding.AwayFromZero); // didn't find how to round this tax

        // Calculate after-tax income
        decimal afterTaxIncome = baseSalary - totalTax;

        return new SalaryDetails
        {
            BaseSalary = baseSalary,
            Superannuation = RoundToNearestCent(baseSalary * 0.095m),
            Taxes = new Taxes
            {
                Income = incomeTax,
                Medicare = medicareTax,
                Total = totalTax
            },
            AfterTaxIncome = afterTaxIncome
        };
    }

    public SalaryDetails CalculatePreTaxIncome(decimal postTaxSalary)
    {
        // Use the loaders to get tax brackets
        var taxBrackets = _taxBracketsLoader.LoadTaxBrackets();

        var baseSalary = CalculatePreTaxIncome(postTaxSalary, taxBrackets);

        return CalculateTaxes(baseSalary);
    }

    private decimal CalculateIncomeTax(decimal baseSalary, IEnumerable<TaxBracket> taxBrackets)
    {
        decimal incomeTax = 0;
        decimal previousTreshold = 0;
        baseSalary = Math.Floor(baseSalary); // Round down salary

        // Iterate through tax brackets
        foreach (var bracket in taxBrackets)
        {
            if (baseSalary > bracket.Threshold)
            {
                // Calculate tax for the current bracket
                incomeTax += AtoRound((Math.Min(baseSalary, bracket.Threshold) - previousTreshold - 1) * bracket.Rate);
                previousTreshold = bracket.Threshold;
            }
            else
            {
                // Calculate remaining tax and exit loop
                incomeTax += AtoRound((baseSalary - previousTreshold) * bracket.Rate);
                break;
            }
        }

        return AtoRound(incomeTax); // Return rounded tax
    }

    public decimal CalculatePreTaxIncome(decimal postTaxSalary, IEnumerable<TaxBracket> taxBrackets)
    {
        // There may be a more elegant way to calculate pre-tax salary from a post-tax value, 
        // but given the time constraints, I used a quick method that leverages the existing 
        // and tested CalculateIncomeTax() function. It iteratively guesses the base salary 
        // and validates it using the tax calculation.

        decimal low = postTaxSalary;
        decimal high = postTaxSalary * 2;  // Upper estimate for base salary
        decimal tolerance = 1; // Accuracy within 1 dollar

        while (high - low > tolerance)
        {
            decimal guessSalary = (low + high) / 2;
            decimal calculatedAfterTaxIncome = CalculateTaxes(guessSalary).AfterTaxIncome;

            if (calculatedAfterTaxIncome > postTaxSalary)
            {
                high = guessSalary; // Lower base salary guess
            }
            else
            {
                low = guessSalary; // Increase base salary guess
            }
        }

        return Math.Round((low + high) / 2, 0); // Return estimated pre-tax salary
    }

    public static decimal AtoRound(decimal value)
    {
        // Find the fractional part of the number
        decimal fractionalPart = value - Math.Floor(value);

        // Check if the fractional part is greater than 0.159
        if (fractionalPart > 0.159m)
        {
            // Round up the number
            return Math.Ceiling(value);
        }

        // Otherwise, round down
        return Math.Floor(value);
    }

    private decimal CalculateMedicareTax(decimal baseSalary)
    {
        var medicareLevy = baseSalary > 26668
            ? baseSalary * .02m
            : baseSalary > 21336
                ? (baseSalary - 21336) * .1m
                : 0;

        return RoundToNearestCent(medicareLevy); // Ensure the final tax is rounded to the nearest cent
    }

    public static decimal RoundToNearestCent(decimal value)
    {
        // Round to the nearest cent (2 decimal places)
        return Math.Round(value, 2, MidpointRounding.AwayFromZero);
    }
}