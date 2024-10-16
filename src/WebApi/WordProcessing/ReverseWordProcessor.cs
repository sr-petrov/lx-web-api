namespace WebApi.WordProcessing;

/// <summary>
/// Implements the <see cref="IWordProcessorStrategy"/> interface to reverse
/// a specified segment of a character array.
/// </summary>
/// <remarks>
/// This class provides the functionality to reverse the characters within
/// the specified range of the character array, defined by the <paramref name="startIndex"/>
/// and <paramref name="length"/> parameters.
/// </remarks>
public class ReverseWordProcessor : IWordProcessorStrategy
{
    public void Process(char[] charArray, int startIndex, int length)
    {
        Array.Reverse(charArray, startIndex, length);
    }
}
