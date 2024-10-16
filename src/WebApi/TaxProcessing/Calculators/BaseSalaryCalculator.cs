namespace WebApi.TaxProcessing.Calculators;


/// <summary>
/// Implements the ISalaryCalculatorStrategy interface to calculate salary details based on a base salary.
/// It computes income tax using tax brackets and Medicare tax, returning a SalaryDetails object
/// that includes the base salary, superannuation, taxes, total tax, and after-tax income.
/// </summary>
public class BaseSalaryCalculator : ISalaryCalculatorStrategy
{
    private readonly TaxBracketsLoader _taxBracketsLoader;
    private readonly MedicareTaxCalculator _medicareTaxCalculator;

    public BaseSalaryCalculator(TaxBracketsLoader taxBracketsLoader, MedicareTaxCalculator medicareTaxCalculator)
    {
        _taxBracketsLoader = taxBracketsLoader;
        _medicareTaxCalculator = medicareTaxCalculator;
    }

    public SalaryDetails CalculateSalaryDetails(decimal baseSalary)
    {
        // Calculate income tax based on tax brackets
        decimal incomeTax = CalculateIncomeTax(baseSalary);

        // Calculate Medicare based on rules
        decimal medicareTax = _medicareTaxCalculator.Calculate(baseSalary);

        // Total tax is the sum of income tax and Medicare
        decimal totalTax = Math.Round(incomeTax + medicareTax, 0, MidpointRounding.AwayFromZero);

        // Calculate after-tax income
        decimal afterTaxIncome = baseSalary - totalTax;

        return new SalaryDetails
        {
            BaseSalary = baseSalary,
            Superannuation = RoundingHelper.RoundToNearestCent(baseSalary * 0.095m),
            Taxes = new Taxes
            {
                Income = incomeTax,
                Medicare = medicareTax,
                Total = totalTax
            },
            AfterTaxIncome = afterTaxIncome
        };
    }

    private decimal CalculateIncomeTax(decimal baseSalary)
    {
        decimal incomeTax = 0;
        decimal previousThreshold = 0;
        baseSalary = Math.Floor(baseSalary); // Round down salary

        // Use the loaders to get tax brackets
        var taxBrackets = _taxBracketsLoader.LoadTaxBrackets();

        // Iterate through tax brackets
        foreach (var bracket in taxBrackets)
        {
            if (baseSalary > bracket.Threshold)
            {
                // Calculate tax for the current bracket
                incomeTax += RoundingHelper.AtoRound((Math.Min(baseSalary, bracket.Threshold) - previousThreshold - 1) * bracket.Rate);
                previousThreshold = bracket.Threshold;
            }
            else
            {
                // Calculate remaining tax and exit loop
                incomeTax += RoundingHelper.AtoRound((baseSalary - previousThreshold) * bracket.Rate);
                break;
            }
        }

        return RoundingHelper.AtoRound(incomeTax); // Return rounded tax
    }
}