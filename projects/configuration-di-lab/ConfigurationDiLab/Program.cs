using ConfigurationDiLab.Contracts;
using Microsoft.AspNetCore.Mvc;

var hourlyRates = new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase)
{
    ["suzuki"] = 10000m,
    ["honda"] = 12000m,
    ["kawasaki"] = 15000m,
    ["ducati"] = 18000m,
    ["aprilia"] = 20000m
};

// M1: itt állítsd össze kézzel a kalkulátor függőségeit.

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

var app = builder.Build();

app.UseExceptionHandler();
app.UseStatusCodePages();

app.MapGet("/", () => Results.Ok(new { application = "ConfigurationDiLab" }));

app.MapGet("/services/wage-calculation/{bikeBrand}",
    (string bikeBrand, [FromQuery] decimal workHours) =>
    {
        var normalizedBikeBrand = bikeBrand.ToLowerInvariant();

        if (!hourlyRates.TryGetValue(normalizedBikeBrand, out var hourlyRate))
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
            // M1: ezt a számítást helyettesítsd a kalkulátor hívásával.
            TotalCost = Math.Ceiling(hourlyRate * workHours),
            Currency = "HUF"
        });
    });

app.Run();

public partial class Program { }
