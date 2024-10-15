namespace WebApi.Taxes;
public class SalaryDetails
{
    public decimal BaseSalary { get; set; }
    public decimal Superannuation { get; set; }
    public Taxes Taxes { get; set; }
    public decimal AfterTaxIncome { get; set; }
}