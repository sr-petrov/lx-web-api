namespace WebApi.WordProcessing;

/// <summary>
/// Implements the <see cref="IWordProcessorStrategy"/> interface to sort
/// a specified segment of a character array in a case-insensitive manner.
/// </summary>
/// <remarks>
/// This class provides the functionality to sort the characters within
/// the specified range of the character array, defined by the <paramref name="startIndex"/>
/// and <paramref name="length"/> parameters. The sorting is done in a case-insensitive
/// manner, ensuring that uppercase and lowercase letters are treated equally.
/// </remarks>
public class SortWordProcessor : IWordProcessorStrategy
{
    public void Process(char[] charArray, int startIndex, int length)
    {
        char[] subArray = new char[length];
        Array.Copy(charArray, startIndex, subArray, 0, length);
        Array.Sort(subArray, (a, b) => char.ToLower(a).CompareTo(char.ToLower(b)));
        Array.Copy(subArray, 0, charArray, startIndex, length);
    }
}