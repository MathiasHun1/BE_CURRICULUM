using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
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

    [Fact]
    public async Task RespondWithBadRequest_WhenInvalidBikeBrandIsProvided()
    {
        // Arrange
        var client = _factory.CreateClient();
        // Act
        var response = await client.GetAsync("/services/wage-calculation/bmw?workHours=1.5");
        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("application/problem+json", response.Content.Headers.ContentType?.MediaType);

        var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        Assert.NotNull(problem);
        Assert.Equal("urn:problem:unsupported-bike-brand", problem.Type);
        Assert.Equal("Unsupported bike brand", problem.Title);
        Assert.Equal("Choose another brand", problem.Detail);
        Assert.Equal(400, problem.Status);
    }

    [Fact]
    public async Task RespondWithBadRequest_WhenInvalidWorkHours_Scenario_1()
    {
        // Arrange
        var client = _factory.CreateClient();
        // Act
        var response = await client.GetAsync("/services/wage-calculation/suzuki?workHours=0");
        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("application/problem+json", response.Content.Headers.ContentType?.MediaType);

        var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        Assert.NotNull(problem);
        Assert.Equal("urn:problem:invalid-work-hours", problem.Type);
        Assert.Equal("Invalid work hours", problem.Title);
        Assert.Equal("Work hours must be greater than 0 and at most 200", problem.Detail);
        Assert.Equal(400, problem.Status);
    }

    [Fact]
    public async Task RespondWithBadRequest_WhenInvalidWorkHours_Scenario_2()
    {
        // Arrange
        var client = _factory.CreateClient();
        // Act
        var response = await client.GetAsync("/services/wage-calculation/suzuki?workHours=200.5");
        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("application/problem+json", response.Content.Headers.ContentType?.MediaType);

        var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        Assert.NotNull(problem);
        Assert.Equal("urn:problem:invalid-work-hours", problem.Type);
        Assert.Equal("Invalid work hours", problem.Title);
        Assert.Equal("Work hours must be greater than 0 and at most 200", problem.Detail);
        Assert.Equal(400, problem.Status);
    }

    [Fact]
    public async Task RespondWithNotFound_WhenRouteSegmentIsNotFound()
    {
        // Arrange
        var client = _factory.CreateClient();
        // Act
        var response = await client.GetAsync("/services/wage-calculation?workHours=1.25");
        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.Equal("application/problem+json", response.Content.Headers.ContentType?.MediaType);

        var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        Assert.NotNull(problem);
        Assert.Equal("about:blank", problem.Type);
        Assert.Equal("Not Found", problem.Title);
        Assert.Equal(404, problem.Status);
    }
}
