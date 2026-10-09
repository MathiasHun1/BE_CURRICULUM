using ConfigurationDiLab.Configuration;
using ConfigurationDiLab.Contracts;
using ConfigurationDiLab.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

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
builder.Services
    .AddOptions<PricingOptions>()
    .Bind(builder.Configuration.GetSection("Pricing"))
    .Validate((pricingOptions) =>
    {
        if (pricingOptions.HourlyRates is null)
        {
            return false;      
        }

        foreach (var (key, value) in pricingOptions.HourlyRates)
        {
            if (value <= 0)
            {
                return false;

            }
        }

        if (pricingOptions.HourlyRates.Count == 0)
        {
            return false;
        }
        return true;
    }, "Pricing options are invalid")
    .ValidateOnStart();

builder.Services.AddScoped<ICostCalculatorService, CostCalculatorService>();
builder.Services.AddTransient<LifeTimeProbe>();

var app = builder.Build();

app.UseExceptionHandler();
app.UseStatusCodePages();


app.MapGet("/", () => Results.Ok(new { application = "ConfigurationDiLab" }));

app.MapGet("/services/wage-calculation/{bikeBrand}",
    (string bikeBrand, [FromQuery] decimal workHours, [FromServices] ICostCalculatorService calculator, [FromServices] IOptionsSnapshot<PricingOptions> pricingOptions) =>
    {
        var normalizedBikeBrand = bikeBrand.ToLowerInvariant();

        if (!pricingOptions.Value.HourlyRates.TryGetValue(normalizedBikeBrand, out var hourlyRate))
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

app.MapGet("/debug/lifetime", ([FromServices] LifeTimeProbe probeOne, [FromServices] LifeTimeProbe probeTwo) =>
{
    var firstIDep = probeOne.InstanceId;
    var secondIDep = probeTwo.InstanceId;
    var sameInstance = Object.ReferenceEquals(probeOne, probeTwo);
    return Results.Ok(new { firstIDep, secondIDep, sameInstance });
});

app.Run();

public partial class Program { }
