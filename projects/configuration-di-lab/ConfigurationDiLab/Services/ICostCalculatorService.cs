namespace ConfigurationDiLab.Services;

public interface ICostCalculatorService
{
    public decimal Calculate(string bikeBrand, decimal workHours);
}