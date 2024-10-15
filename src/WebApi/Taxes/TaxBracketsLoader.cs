namespace WebApi.Taxes;

public class TaxBracketsLoader
{
    public IEnumerable<TaxBracket> LoadTaxBrackets()
    {
        // Load your tax brackets from the predefined configuration file or database.
        return new List<TaxBracket>()
        {
            new TaxBracket { Threshold = 18200, Rate = 0m },
            new TaxBracket { Threshold = 37000, Rate = 0.19m },
            new TaxBracket { Threshold = 87000, Rate = 0.325m },
            new TaxBracket { Threshold = 180000, Rate =  0.37m },
            new TaxBracket { Threshold = decimal.MaxValue, Rate = 0.45m },
        };
    }
}
