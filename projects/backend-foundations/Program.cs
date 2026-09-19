using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<RouteHandlerOptions>(options =>
{
    options.ThrowOnBadRequest = false;
});
var app = builder.Build();

app.Use(async (context, next) =>
{
    try
    {
        await next(context);
    }
    catch (Exception e)
    {
        app.Logger.LogError(e, "An unhandled exception occured while processing {Method} {Path}", context.Request.Method, context.Request.Path);
        if (context.Response.HasStarted)
        {
            throw;
        }
        
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await context.Response.WriteAsJsonAsync(new { error = "An unhandled exception has occurred while processing your request."});
    }
});

app.Use(async (context, next) =>
{
    app.Logger.LogInformation("HTTP request received. Method: {HttpMethod}",  context.Request.Method);
    app.Logger.LogInformation("HTTP request received. Path: {Path}",  context.Request.Path);
    
    await next(context);
    app.Logger.LogInformation("HTTP response sent. Status code: {StatusCode}", context.Response.StatusCode);
});

app.Use(async (context, next) =>
{
    if (context.Request.Path.StartsWithSegments("/short-circuit")) {
        context.Response.StatusCode = 418;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(new { message = "route short-circuited by middleware" });
        
        return;
    }
    
    await next(context);
});

// -----------Routes -----------//
app.MapGet("/", () => "Hello World!");
app.MapGet("/health", () =>
{
    var response = new { status = "ok" };
    return response;
});

app.MapGet("/greet/{name}", (string name) => new { message = $"Hello, {name}!"});

app.MapGet("/fail", () =>
{
    throw new InvalidOperationException("This is a test exception");
});

app.MapGet("/partial-fail", async (context) =>
{
    await context.Response.WriteAsync("Partial fail text");
    await context.Response.Body.FlushAsync();
    throw new InvalidOperationException("This is a test exception");
});

app.MapGet("/divide", (int dividend, int divisor ) => {
    if (divisor == 0)
    {
        return Results.BadRequest("Divisor cannot be zero");
    }
    
    return Results.Ok(dividend / divisor);
});

app.MapGet("/rectangle/{width}", (int width, [FromQuery] int height) =>
{
    if (width <= 0 || height <= 0)
    {
        return Results.BadRequest();
    }
    
    var area = width * height;
    return Results.Ok(new { width, height, area });
});

app.Run();
