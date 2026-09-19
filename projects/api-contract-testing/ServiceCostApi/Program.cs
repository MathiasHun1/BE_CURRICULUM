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
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/services/wage-calculation/{bikeBrand}", (string bikeBrand, [FromQuery] decimal workHours) =>
{
  var normalizedBikeBrand = bikeBrand.ToLower();
  decimal hourlyRate;
  
  if (!hourlyRates.TryGetValue(normalizedBikeBrand, out hourlyRate))
  {
    return Results.BadRequest("Invalid bike brand");
  }

  if (workHours <= 0 || workHours > 200)
  {
    return Results.BadRequest("Invalid work hours");
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

