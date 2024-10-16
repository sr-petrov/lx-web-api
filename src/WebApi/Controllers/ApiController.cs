using Microsoft.AspNetCore.Mvc;
using WebApi.TaxProcessing;
using WebApi.TaxProcessing.Calculators;
using WebApi.WordProcessing;

namespace WebApi.Controllers;

[ApiController]
[Route("api")]
public class ApiController : ControllerBase
{
    private readonly ITextProcessingService _textProcessingService;
    private readonly TaxCalculator _taxCalculator;

    public ApiController(ITextProcessingService textProcessingService, TaxCalculator taxCalculator)
    {
        _textProcessingService = textProcessingService;
        _taxCalculator = taxCalculator;
    }

    // Reverses the words in the given sentence.
    [HttpGet("reverse-words")]
    public string ReverseWords(string sentence)
    {
        return _textProcessingService.Process(sentence, new ReverseWordProcessor());
    }

    // Sorts the letters of each word in the given sentence.
    [HttpGet("sort-words")]
    public string SortWords(string sentence)
    {
        return _textProcessingService.Process(sentence, new SortWordProcessor());
    }

    // Calculates and returns the details of after-tax income based on the provided income.
    [HttpGet("calculate-after-tax-income")]
    public SalaryDetails CalculateAfterTaxIncome(decimal annualBaseSalary)
    {
        return _taxCalculator.CalculateByBaseSalary(annualBaseSalary);
    }

    // Calculates and returns the pre-tax income based on the given post-tax salary.
    [HttpGet("calculate-pre-tax-income-from-take-home")]
    public SalaryDetails CalculatePreTaxIncomeFromTakeHome(decimal postTaxSalary)
    {
        return _taxCalculator.CalculateByPostTaxSalary(postTaxSalary);
    }
}
