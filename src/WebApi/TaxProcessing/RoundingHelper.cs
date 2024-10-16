namespace WebApi.TaxProcessing;

public static class RoundingHelper
{
    public static decimal AtoRound(decimal value)
    {
        // Find the fractional part of the number
        decimal fractionalPart = value - Math.Floor(value);

        // Check if the fractional part is greater than 0.159
        if (fractionalPart > 0.159m)
        {
            // Round up the number
            return Math.Ceiling(value);
        }

        // Otherwise, round down
        return Math.Floor(value);
    }

    public static decimal RoundToNearestCent(decimal value)
    {
        // Round to the nearest cent (2 decimal places)
        return Math.Round(value, 2, MidpointRounding.AwayFromZero);
    }
}