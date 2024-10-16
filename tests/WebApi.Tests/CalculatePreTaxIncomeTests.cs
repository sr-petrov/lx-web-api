using WebApi.TaxProcessing;
using WebApi.TaxProcessing.Calculators;

namespace WebApi.Tests;

public class CalculatePreTaxIncomeTests
{
    private readonly TaxCalculator _taxCalculator;
    public CalculatePreTaxIncomeTests()
    {
        var taxBracketsLoader = new TaxBracketsLoader();
        var medicareTaxCalculator = new MedicareTaxCalculator();

        _taxCalculator = new TaxCalculator(taxBracketsLoader, medicareTaxCalculator);
    }

    [Fact]
    public void Calculate85000Test()
    {
        var result = _taxCalculator.CalculateByPostTaxSalary(85000);

        Assert.Equal(119069, result.BaseSalary);
        Assert.Equal(11311.56m, result.Superannuation);
        Assert.Equal(31688, result.Taxes.Income);
        Assert.Equal(2381.38m, result.Taxes.Medicare);
        Assert.Equal(34069, result.Taxes.Total);
        Assert.Equal(85000, result.AfterTaxIncome);
    }

    [Fact]
    public void Calculate64000Test()
    {
        var result = _taxCalculator.CalculateByPostTaxSalary(64000);

        Assert.Equal(84805, result.BaseSalary);
        Assert.Equal(8056.48m, result.Superannuation);
        Assert.Equal(19109, result.Taxes.Income);
        Assert.Equal(1696.1m, result.Taxes.Medicare);
        Assert.Equal(20805, result.Taxes.Total);
        Assert.Equal(64000, result.AfterTaxIncome);
    }

    [Fact]
    public void Calculate15200Test()
    {
        var result = _taxCalculator.CalculateByPostTaxSalary(15200);

        Assert.Equal(15200, result.BaseSalary);
        Assert.Equal(1444, result.Superannuation);
        Assert.Equal(0, result.Taxes.Income);
        Assert.Equal(0, result.Taxes.Medicare);
        Assert.Equal(0, result.Taxes.Total);
        Assert.Equal(15200, result.AfterTaxIncome);
    }

    [Fact]
    public void Calculate194003Test()
    {
        var result = _taxCalculator.CalculateByPostTaxSalary(194003);

        Assert.Equal(315538, result.BaseSalary);
        Assert.Equal(29976.11m, result.Superannuation);
        Assert.Equal(115224, result.Taxes.Income);
        Assert.Equal(6310.76m, result.Taxes.Medicare);
        Assert.Equal(121535, result.Taxes.Total);
        Assert.Equal(194003, result.AfterTaxIncome);
    }
}