namespace WebApi.WordProcessing;

/// <summary>
/// Defines a strategy for processing a segment of a character array.
/// Implementing classes should provide specific logic for manipulating the characters
/// within the specified range of the array, starting from the given index and extending
/// for the specified length.
/// </summary>
public interface IWordProcessorStrategy
{
    void Process(char[] charArray, int startIndex, int length);
}
