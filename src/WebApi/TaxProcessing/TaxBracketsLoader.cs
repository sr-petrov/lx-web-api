namespace WebApi.TaxProcessing;

/// <summary>
/// Contract for loading tax brackets for income calculation.
/// 
/// Implementations can source data from databases, web APIs, or 
/// configuration files, allowing flexibility for different years.
/// </summary>
public interface ITaxBracketsLoader
{
    /// <summary>
    /// Loads tax brackets for income tax calculation.
    /// 
    /// @returns A collection of <see cref="TaxBracket"/> with thresholds and rates.
    /// </summary>
    IEnumerable<TaxBracket> LoadTaxBrackets();
}

/// <summary>
/// Loads tax brackets for income calculation from a predefined 
/// configuration. Can be modified to source data for different 
/// years or from various locations.
/// </summary>
public class TaxBracketsLoader : ITaxBracketsLoader
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

/// <summary>
/// Represents a tax bracket for income tax calculations.
/// 
/// Each bracket defines an income threshold and the corresponding 
/// tax rate. The <see cref="Threshold"/> property indicates the 
/// upper limit for the bracket, while the <see cref="Rate"/> 
/// property specifies the applicable tax rate within this range.
/// </summary>
public class TaxBracket
{
    public decimal Threshold { get; set; }
    public decimal Rate { get; set; }
}