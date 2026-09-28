using System.Globalization;
using System.Net;
using System.Net.Http.Json;
using ConfigurationDiLab.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;

namespace ConfigurationDiLab.Tests;

public sealed class WageCalculationContractTests
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public WageCalculationContractTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Theory]
    [InlineData("ducati", "1.5", "18000", "27000")]
    [InlineData("DUCATI", "1.5", "18000", "27000")]
    [InlineData("suzuki", "0.00001", "10000", "1")]
    public async Task ValidInput_ReturnsExpectedCalculation(
        string brand, string hours, string rate, string total)
    {
        using var client = _factory.CreateClient();
        using var response = await client.GetAsync(
            $"/services/wage-calculation/{brand}?workHours={hours}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("application/json", response.Content.Headers.ContentType?.MediaType);

        var calculation = await response.Content.ReadFromJsonAsync<WageCalculationResponse>();
        Assert.NotNull(calculation);
        Assert.Equal(brand.ToLowerInvariant(), calculation.BikeBrand);
        Assert.Equal(decimal.Parse(hours, CultureInfo.InvariantCulture), calculation.WorkHours);
        Assert.Equal(decimal.Parse(rate, CultureInfo.InvariantCulture), calculation.AppliedHourlyRate);
        Assert.Equal(decimal.Parse(total, CultureInfo.InvariantCulture), calculation.TotalCost);
        Assert.Equal("HUF", calculation.Currency);
    }

    [Theory]
    [InlineData("0")]
    [InlineData("-1")]
    [InlineData("200.5")]
    public async Task InvalidHours_ReturnsBusinessProblem(string hours)
    {
        using var client = _factory.CreateClient();
        using var response = await client.GetAsync(
            $"/services/wage-calculation/ducati?workHours={hours}");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("application/problem+json", response.Content.Headers.ContentType?.MediaType);

        var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        Assert.NotNull(problem);
        Assert.Equal(400, problem.Status);
        Assert.Equal("urn:problem:invalid-work-hours", problem.Type);
        Assert.Equal("Invalid work hours", problem.Title);
        Assert.Equal("Work hours must be greater than 0 and at most 200", problem.Detail);
    }

    [Fact]
    public async Task UnsupportedBrand_ReturnsBusinessProblem()
    {
        using var client = _factory.CreateClient();
        using var response = await client.GetAsync(
            "/services/wage-calculation/bmw?workHours=1.5");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("application/problem+json", response.Content.Headers.ContentType?.MediaType);

        var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        Assert.NotNull(problem);
        Assert.Equal(400, problem.Status);
        Assert.Equal("urn:problem:unsupported-bike-brand", problem.Type);
        Assert.Equal("Unsupported bike brand", problem.Title);
        Assert.Equal("Choose another brand", problem.Detail);
    }

    [Fact]
    public async Task MissingBrandSegment_ReturnsNotFoundProblem()
    {
        using var client = _factory.CreateClient();
        using var response = await client.GetAsync(
            "/services/wage-calculation?workHours=1.5");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.Equal("application/problem+json", response.Content.Headers.ContentType?.MediaType);

        var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        Assert.NotNull(problem);
        Assert.Equal(404, problem.Status);
        Assert.Equal("about:blank", problem.Type);
        Assert.Equal("Not Found", problem.Title);
    }

    [Fact]
    public async Task NonNumericHours_ReturnsBindingBadRequest()
    {
        using var client = _factory.CreateClient();
        using var response = await client.GetAsync(
            "/services/wage-calculation/ducati?workHours=abc");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
