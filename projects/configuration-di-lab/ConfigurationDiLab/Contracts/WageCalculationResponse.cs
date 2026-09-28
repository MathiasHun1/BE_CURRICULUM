namespace ConfigurationDiLab.Contracts;

public sealed class WageCalculationResponse
{
    public string BikeBrand { get; init; } = string.Empty;
    public decimal WorkHours { get; init; }
    public decimal AppliedHourlyRate { get; init; }
    public decimal TotalCost { get; init; }
    public string Currency { get; init; } = string.Empty;
}
