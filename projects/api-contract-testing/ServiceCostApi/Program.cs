using Microsoft.AspNetCore.Mvc;
using ServiceCostApi.Contracts;

var hourlyRates = new Dictionary<string, decimal>
{
  ["suzuki"] = 10000m,
  ["honda"] = 12000m,
  ["kawasaki"] = 15000m,
  ["ducati"] = 18000m,
  ["aprilia"] = 20000m
};


var builder = WebApplication.CreateBuilder(args);
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

app.UseStatusCodePages();

app.MapGet("/", () => "Hello World!");
app.MapGet("/services/wage-calculation/{bikeBrand}", (string bikeBrand, [FromQuery] decimal workHours) =>
{
  var normalizedBikeBrand = bikeBrand.ToLower();
  decimal hourlyRate;
  
  if (!hourlyRates.TryGetValue(normalizedBikeBrand, out hourlyRate))
  {
    return Results.Problem(title: "Unsupported bike brand", detail: "Choose another brand",
      type: "urn:problem:unsupported-bike-brand", statusCode: 400);
  }

  if (workHours <= 0 || workHours > 200)
  {
    return Results.Problem(
      title: "Invalid work hours",
      detail: "Work hours must be greater than 0 and at most 200",
      type: "urn:problem:invalid-work-hours",
      statusCode: 400);
  }
  
  return Results.Ok(new WageCalculationResponse
  {
    BikeBrand = normalizedBikeBrand,
    WorkHours = workHours,
    AppliedHourlyRate = hourlyRate,
    TotalCost = Math.Ceiling(hourlyRate * workHours),
    Currency = "HUF"
  });
});


app.Run();

public partial class Program {}

