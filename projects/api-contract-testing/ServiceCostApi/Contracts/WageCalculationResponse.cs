namespace ServiceCostApi.Contracts;

public class WageCalculationResponse
{
    public string BikeBrand { get; set; } = string.Empty;
    public decimal WorkHours { get; set; }
    public decimal AppliedHourlyRate { get; set; }
    public decimal TotalCost { get; set; }
    public string Currency { get; set; } = string.Empty;
}