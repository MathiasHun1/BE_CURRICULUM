using ConfigurationDiLab.Configuration;
using ConfigurationDiLab.Contracts;
using ConfigurationDiLab.Services;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

var pricingOptions = builder.Configuration
    .GetSection("Pricing")
    .Get<PricingOptions>();

if (pricingOptions is null)
{
    throw new InvalidOperationException("PricingOptions not found");
}

if (pricingOptions.HourlyRates.Count == 0)
{
    throw new InvalidOperationException("No hourly rates found");   
}

builder.Services.Configure<RouteHandlerOptions>(options =>
    options.ThrowOnBadRequest = false);
builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = context =>
    {
        if (context.HttpContext.Response.StatusCode == StatusCodes.Status404NotFound)
        {
            context.ProblemDetails.Type = "about:blank";
        }
    };
});

builder.Services.AddSingleton<IReadOnlyDictionary<string, decimal>>(pricingOptions.HourlyRates);
builder.Services.AddScoped<ICostCalculatorService, CostCalculatorService>();

var app = builder.Build();

app.UseExceptionHandler();
app.UseStatusCodePages();


app.MapGet("/", () => Results.Ok(new { application = "ConfigurationDiLab" }));

app.MapGet("/services/wage-calculation/{bikeBrand}",
    (string bikeBrand, [FromQuery] decimal workHours, [FromServices] ICostCalculatorService calculator) =>
    {
        var normalizedBikeBrand = bikeBrand.ToLowerInvariant();

        if (!pricingOptions.HourlyRates.TryGetValue(normalizedBikeBrand, out var hourlyRate))
        {
            return Results.Problem(
                title: "Unsupported bike brand",
                detail: "Choose another brand",
                type: "urn:problem:unsupported-bike-brand",
                statusCode: StatusCodes.Status400BadRequest);
        }

        if (workHours <= 0 || workHours > 200)
        {
            return Results.Problem(
                title: "Invalid work hours",
                detail: "Work hours must be greater than 0 and at most 200",
                type: "urn:problem:invalid-work-hours",
                statusCode: StatusCodes.Status400BadRequest);
        }

        return Results.Ok(new WageCalculationResponse
        {
            BikeBrand = normalizedBikeBrand,
            WorkHours = workHours,
            AppliedHourlyRate = hourlyRate,
            TotalCost = calculator.Calculate(normalizedBikeBrand, workHours),
            Currency = "HUF"
        });
    });

app.Run();

public partial class Program { }
