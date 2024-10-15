using Microsoft.AspNetCore.Mvc;

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

            for(int j = 0; j < charArray.Length; j++)
            {
                // Mark the start index of the word when a valid character is found
                if (IsLetterOrApostrophe(charArray[j]) && wordStartIndex == -1)
                {
                    wordStartIndex = j;
                }

                // Reverse the word when a non-valid character is encountered
                if (!IsLetterOrApostrophe(charArray[j]) && wordStartIndex != -1)
                {
                    Array.Reverse(charArray, wordStartIndex, j - wordStartIndex);
                    wordStartIndex = -1; // Reset the start index for the next word
                }
            }

            if (wordStartIndex > -1 && charArray.Length > wordStartIndex)
            {
                // Reverse the last word segment if it ends at the end of the character array
                Array.Reverse(charArray, wordStartIndex, charArray.Length - wordStartIndex);
            }

            // Create a new string from the reversed character array and update the word in the array
            words[i] = new string(charArray);
        }

        // Join the array of words back into a single string, ensuring words are separated by spaces
        return string.Join(" ", words);
    }

    private bool IsLetterOrApostrophe(char c)
    {
        // Check if the character is a letter or an apostrophe
        return char.IsLetter(c) || c == '\'';
    }
}
