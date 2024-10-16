namespace WebApi.TaxProcessing.Calculators;

/// <summary>
/// Strategy for calculating salary details based on a post-tax salary.
/// It estimates the corresponding pre-tax salary and uses the BaseSalaryCalculator 
/// to derive the full salary details.
/// </summary>
public class PostTaxSalaryCalculator : ISalaryCalculatorStrategy
{
    private readonly BaseSalaryCalculator _baseSalaryCalculator;

    public PostTaxSalaryCalculator(BaseSalaryCalculator baseSalaryCalculator)
    {
        _baseSalaryCalculator = baseSalaryCalculator;
    }

    public SalaryDetails CalculateSalaryDetails(decimal postTaxSalary)
    {
        var baseSalary = CalculatePreTaxIncome(postTaxSalary);
        return _baseSalaryCalculator.CalculateSalaryDetails(baseSalary);
    }

    private decimal CalculatePreTaxIncome(decimal postTaxSalary)
    {
        decimal low = postTaxSalary;
        decimal high = postTaxSalary * 2;  // Upper estimate for base salary
        decimal tolerance = 1; // Accuracy within 1 dollar

        while (high - low > tolerance)
        {
            decimal guessSalary = (low + high) / 2;
            decimal calculatedAfterTaxIncome = _baseSalaryCalculator.CalculateSalaryDetails(guessSalary).AfterTaxIncome;

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
}
