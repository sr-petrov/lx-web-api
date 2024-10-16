namespace WebApi.WordProcessing;

/// <summary>
/// Defines the contract for a text processing service that modifies
/// strings using specified word processing strategies.
/// </summary>
public interface ITextProcessingService
{
    public string Process(string sentence, IWordProcessorStrategy strategy);
}
