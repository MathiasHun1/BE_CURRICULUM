using System.Net;
using System.Net.Http.Json;
using ServiceCostApi.Contracts;
using Microsoft.AspNetCore.Mvc.Testing;

namespace ServiceCostApi.Tests;

public class WageCalculationContractTests
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public WageCalculationContractTests(
        WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetWageCalculation_WithValidDucatiInput_ReturnsOk()
    {
        // Arrange
        using var client = _factory.CreateClient();
        // Act
        var response = await client.GetAsync(
            "/services/wage-calculation/ducati?workHours=1.5");
        var result =
            await response.Content.ReadFromJsonAsync<WageCalculationResponse>();
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("application/json", response.Content.Headers.ContentType?.MediaType);
        
        Assert.NotNull(result);
        Assert.Equal("ducati", result.BikeBrand);
        Assert.Equal(1.5m, result.WorkHours);
        Assert.Equal(18000m, result.AppliedHourlyRate);
        Assert.Equal(27000m, result.TotalCost);
        Assert.Equal("HUF", result.Currency);
    }
}