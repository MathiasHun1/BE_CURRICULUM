namespace ConfigurationDiLab.Configuration;

public class PricingOptions
{
    public Dictionary<string, decimal> HourlyRates { get; set; } = new(StringComparer.OrdinalIgnoreCase);
}