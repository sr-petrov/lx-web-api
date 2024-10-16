namespace WebApi.WordProcessing;

/// <summary>
/// Implements <see cref="ITextProcessingService"/> to process sentences
/// using word processing strategies.
/// </summary>
/// <remarks>
/// This class splits a sentence into words, applies a specified
/// <see cref="IWordProcessorStrategy"/> to each word, and returns
/// the modified sentence.
/// </remarks>
public class TextProcessingService: ITextProcessingService
{
    public string Process(string sentence, IWordProcessorStrategy strategy)
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

    private bool IsLetterOrApostrophe(char c)
    {
        // Check if the character is a letter or an apostrophe
        return char.IsLetter(c) || c == '\'';
    }
}
