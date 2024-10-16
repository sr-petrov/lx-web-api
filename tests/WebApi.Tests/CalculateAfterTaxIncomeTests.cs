﻿using WebApi.TaxProcessing;
using WebApi.TaxProcessing.Calculators;

namespace WebApi.Tests;

public class CalculateAfterTaxIncomeTests
{
    private readonly TaxCalculator _taxCalculator;
    public CalculateAfterTaxIncomeTests()
    {
        var taxBracketsLoader = new TaxBracketsLoader();
        var medicareTaxCalculator = new MedicareTaxCalculator();

        _taxCalculator = new TaxCalculator(taxBracketsLoader, medicareTaxCalculator);
    }

    [Fact]
    public void Calculate85000Test()
    {
        var result = _taxCalculator.CalculateByBaseSalary(85000);

        Assert.Equal(85000, result.BaseSalary);
        Assert.Equal(8075, result.Superannuation);
        Assert.Equal(19172, result.Taxes.Income);
        Assert.Equal(1700, result.Taxes.Medicare);
        Assert.Equal(20872, result.Taxes.Total);
        Assert.Equal(64128, result.AfterTaxIncome);
    }

    [Fact]
    public void Calculate15200Test()
    {
        var result = _taxCalculator.CalculateByBaseSalary(15200);

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
        var result = _taxCalculator.CalculateByBaseSalary(194003);

        Assert.Equal(194003, result.BaseSalary);
        Assert.Equal(18430.29m, result.Superannuation);
        Assert.Equal(60534, result.Taxes.Income);
        Assert.Equal(3880.06m, result.Taxes.Medicare);
        Assert.Equal(64414, result.Taxes.Total);
        Assert.Equal(129589, result.AfterTaxIncome);
    }

    [Fact]
    public void Calculate87000_50Test()
    {
        var result = _taxCalculator.CalculateByBaseSalary(87000.50m);

        Assert.Equal(87000.50m, result.BaseSalary);
        Assert.Equal(8265.05m, result.Superannuation);
        Assert.Equal(19822, result.Taxes.Income);
        Assert.Equal(1740.01m, result.Taxes.Medicare);
        Assert.Equal(21562, result.Taxes.Total);
        Assert.Equal(65438.5m, result.AfterTaxIncome);
    }
}