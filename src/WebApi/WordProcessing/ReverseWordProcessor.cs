namespace WebApi.WordProcessing;

public class ReverseWordProcessor : IWordProcessorStrategy
{
    public void Process(char[] charArray, int startIndex, int length)
    {
        Array.Reverse(charArray, startIndex, length);
    }
}
