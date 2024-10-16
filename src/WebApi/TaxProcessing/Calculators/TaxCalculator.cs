namespace WebApi.TaxProcessing.Calculators;

/// <summary>
/// Facilitates tax calculations by leveraging salary calculator strategies.
/// It supports calculations based on both base salary and post-tax salary,
/// utilizing the BaseSalaryCalculator and PostTaxSalaryCalculator strategies.
/// </summary>
public class TaxCalculator
{
    private readonly ISalaryCalculatorStrategy _baseSalaryCalculator;
    private readonly ISalaryCalculatorStrategy _postTaxSalaryCalculator;

    public TaxCalculator(TaxBracketsLoader taxBracketsLoader, MedicareTaxCalculator medicareTaxCalculator)
    {
        var baseSalaryCalculator = new BaseSalaryCalculator(taxBracketsLoader, medicareTaxCalculator);

        _baseSalaryCalculator = baseSalaryCalculator;
        _postTaxSalaryCalculator = new PostTaxSalaryCalculator(baseSalaryCalculator);
    }

    /// <summary>
    /// Calculates salary details based on the provided base salary.
    /// </summary>
    public SalaryDetails CalculateByBaseSalary(decimal baseSalary)
    {
        return _baseSalaryCalculator.CalculateSalaryDetails(baseSalary);
    }

    /// <summary>
    /// Calculates salary details based on the provided post-tax salary.
    /// </summary>
    public SalaryDetails CalculateByPostTaxSalary(decimal postTaxSalary)
    {
        return _postTaxSalaryCalculator.CalculateSalaryDetails(postTaxSalary);
    }
}
