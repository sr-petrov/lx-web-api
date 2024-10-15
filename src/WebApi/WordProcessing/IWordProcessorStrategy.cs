namespace WebApi.WordProcessing;

public interface IWordProcessorStrategy
{
    void Process(char[] charArray, int startIndex, int length);
}
