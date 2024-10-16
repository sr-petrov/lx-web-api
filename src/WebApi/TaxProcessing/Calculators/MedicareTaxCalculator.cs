namespace WebApi.TaxProcessing.Calculators;


/// <summary>
/// Calculates the Medicare tax based on an individual's base salary.
/// 
/// The calculation considers predefined income thresholds and applies 
/// the corresponding rates to determine the Medicare levy, which is 
/// then rounded to the nearest cent.
/// </summary>
public class MedicareTaxCalculator
{
    // Constants for Medicare calculation
    private const decimal LowerThreshold1 = 21336m;
    private const decimal LowerThreshold2 = 26668m;
    private const decimal MedicareRate1 = 0.1m; // Rate for income above LowerThreshold1
    private const decimal MedicareRate2 = 0.02m; // Rate for income above LowerThreshold2

    public decimal Calculate(decimal baseSalary)
    {
        decimal medicareLevy = baseSalary > LowerThreshold2
            ? baseSalary * MedicareRate2
            : baseSalary > LowerThreshold1
                ? (baseSalary - LowerThreshold1) * MedicareRate1
                : 0;

        return RoundingHelper.RoundToNearestCent(medicareLevy); // Ensure the final tax is rounded to the nearest cent
    }
}