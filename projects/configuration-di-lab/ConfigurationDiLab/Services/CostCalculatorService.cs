using ConfigurationDiLab.Configuration;
using Microsoft.Extensions.Options;

namespace ConfigurationDiLab.Services;

public sealed class CostCalculatorService: ICostCalculatorService
{
    private readonly IReadOnlyDictionary<string, decimal> _hourlyRates;
    
    public CostCalculatorService(IOptionsSnapshot<PricingOptions> pricingOptions)
    {
        _hourlyRates = pricingOptions.Value.HourlyRates;
    }
    
    public decimal Calculate(string bikeBrand, decimal workHours)
    {
        decimal result = Math.Ceiling(_hourlyRates[bikeBrand] * workHours);
        return result;
    }
}
