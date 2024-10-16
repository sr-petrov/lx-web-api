namespace WebApi.TaxProcessing;

/// <summary>
/// Represents an individual's salary information, including the 
/// base salary, superannuation, tax details, and after-tax income.
/// </summary>
public class SalaryDetails
{
    public decimal BaseSalary { get; set; }
    public decimal Superannuation { get; set; }
    public Taxes Taxes { get; set; } = new Taxes();
    public decimal AfterTaxIncome { get; set; }
}

/// <summary>
/// Represents the tax components of an individual's income.
/// Includes income tax, Medicare tax, and total tax amount.
/// </summary>
public class Taxes
{
    public decimal Income { get; set; }
    public decimal Medicare { get; set; }
    public decimal Total { get; set; }
}