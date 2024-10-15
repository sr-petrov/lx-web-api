namespace WebApi.WordProcessing;

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