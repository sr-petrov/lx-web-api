namespace WebApi.TaxProcessing.Calculators;

public interface ISalaryCalculatorStrategy
{
    SalaryDetails CalculateSalaryDetails(decimal salary);
}
