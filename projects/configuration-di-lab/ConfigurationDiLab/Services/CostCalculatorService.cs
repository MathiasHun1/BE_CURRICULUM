namespace ConfigurationDiLab.Services;

public sealed class CostCalculatorService: ICostCalculatorService
{
    private readonly IReadOnlyDictionary<string, decimal> _hourlyRates;
    
    public CostCalculatorService(IReadOnlyDictionary<string, decimal> hourlyRates)
    {
        _hourlyRates = hourlyRates;
    }
    
    public decimal Calculate(string bikeBrand, decimal workHours)
    {
        decimal result = Math.Ceiling(_hourlyRates[bikeBrand] * workHours);
        return result;
    }
}
