using Microsoft.AspNetCore.Mvc;
using WebApi.Taxes;
using WebApi.WordProcessing;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ApiController : ControllerBase
{
    private readonly ILogger<ApiController> _logger;

    public ApiController(ILogger<ApiController> logger)
    {
        _logger = logger;
    }

    [HttpGet("test-get")]
    public string TestGet()
    {
        return "OK";
    }

    [HttpGet("reverse-words")]
    public string ReverseWords(string sentence)
    {
        return ProcessWords(sentence, new ReverseWordProcessor());
    }

    [HttpGet("sort-words")]
    public string SortWords(string sentence)
    {
        return ProcessWords(sentence, new SortWordProcessor());
    }

    [HttpGet("calculate-after-tax-income")]
    public SalaryDetails CalculateAfterTaxIncome(decimal income, [FromServices] TaxCalculator taxCalculator)
    {
        return taxCalculator.CalculateTaxes(income);
    }

    [HttpGet("calculate-pre-tax-income-from-take-home")]
    public SalaryDetails CalculatePreTaxIncomeFromTakeHome(decimal postTaxSalary, [FromServices] TaxCalculator taxCalculator)
    {
        return taxCalculator.CalculatePreTaxIncome(postTaxSalary);
    }


    private bool IsLetterOrApostrophe(char c)
    {
        // Check if the character is a letter or an apostrophe
        return char.IsLetter(c) || c == '\'';
    }

    private string ProcessWords(string sentence, IWordProcessorStrategy strategy)
    {
        if (string.IsNullOrEmpty(sentence)) return string.Empty;

        // Split the input sentence into individual words based on spaces
        string[] words = sentence.Split(' ');

        // Iterate through each word in the array
        for (int i = 0; i < words.Length; i++)
        {
            // Convert the current word into a character array for manipulation
            char[] charArray = words[i].ToCharArray();

            // Initialize index for tracking the start of a word
            var wordStartIndex = -1;

            for (int j = 0; j < charArray.Length; j++)
            {
                // Mark the start index of the word when a valid character is found
                if (IsLetterOrApostrophe(charArray[j]) && wordStartIndex == -1)
                {
                    wordStartIndex = j;
                }

                // Process the word when a non-valid character is encountered
                if (!IsLetterOrApostrophe(charArray[j]) && wordStartIndex != -1)
                {
                    strategy.Process(charArray, wordStartIndex, j - wordStartIndex);
                    wordStartIndex = -1; // Reset the start index for the next word
                }
            }

            if (wordStartIndex != -1 && charArray.Length > wordStartIndex)
            {
                // Process the last word segment if it ends at the end of the character array
                strategy.Process(charArray, wordStartIndex, charArray.Length - wordStartIndex);
            }

            // Create a new string from the processed character array and update the word in the array
            words[i] = new string(charArray);
        }

        // Join the array of words back into a single string, ensuring words are separated by spaces
        return string.Join(" ", words);
    }
}
