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

    private decimal CalculateIncomeTax(decimal baseSalary, IEnumerable<TaxBracket> taxBrackets)
    {
        decimal incomeTax = 0;
        decimal previousTreshold = 0;
        baseSalary = Math.Floor(baseSalary);

        foreach (var bracket in taxBrackets)
        {
            if (baseSalary > bracket.Threshold)
            {
                incomeTax += AtoRound((Math.Min(baseSalary, bracket.Threshold) - previousTreshold - 1) * bracket.Rate);
                previousTreshold = bracket.Threshold;
            }
            else
            {
                incomeTax += AtoRound((baseSalary - previousTreshold) * bracket.Rate);
                break;
            }
        }

        return AtoRound(incomeTax);
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