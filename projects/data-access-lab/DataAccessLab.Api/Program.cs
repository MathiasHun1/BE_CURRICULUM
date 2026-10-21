using DataAccessLab.Api.Data;
using DataAccessLab.Api.Dtos;
using DataAccessLab.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataAccessDbContext>(options =>
{
    var connectionStringBase = builder.Configuration.GetConnectionString("DataAccessLab");
    var dbPassword = builder.Configuration["Database:Password"];
    if (string.IsNullOrEmpty(connectionStringBase) || string.IsNullOrEmpty(dbPassword))
    {
        throw new InvalidOperationException("Database connection string or password is not set");
    }

    var connectionString = new NpgsqlConnectionStringBuilder(connectionStringBase)
    {
        Password = dbPassword
    }.ConnectionString;

    try
    {
        options.UseNpgsql(connectionString);
    } catch (Exception e)
    {
        Console.WriteLine(e);
    }
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/projects/{id:int}", async (DataAccessDbContext context, [FromRoute] int id) =>
{
    // var project = await context.Projects.Select(p => new { p.ProjectId, p.Name }).FirstOrDefaultAsync(p => p.ProjectId == id);
    var project = await context.Projects
        .Where(p => p.ProjectId == id)
        .Select(p => new
        {
            p.ProjectId, 
            p.Name,
            WorkItems = p.WorkItems.Select(w => new { w.WorkItemId, w.Title }).ToList()
        })
        .FirstOrDefaultAsync();
    
    if (project == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(project);
});

app.MapPost("projects/{id:int}/work-items", async (DataAccessDbContext context, [FromRoute] int id, [FromBody] CreateWorkItemRequest request) =>
{
    int projectId = id;

    if (await context.FindAsync<Project>(projectId) == null)
    {
        return Results.NotFound("Project not found");
    }

    if (string.IsNullOrWhiteSpace(request.Title))
    {
        return Results.BadRequest();
    }
    var savedWorkItem = new WorkItem { Title = request.Title, ProjectId = projectId };
    
    context.Add(savedWorkItem);
    await context.SaveChangesAsync();

    return Results.Json(savedWorkItem, statusCode: StatusCodes.Status201Created);

});

app.MapPatch("/projects/{projectId:int}/work-items/{workItemId:int}",
    async (DataAccessDbContext context, int projectId, int workItemId, CreateWorkItemRequest request) =>
    {
        var project = await context.Projects.FirstOrDefaultAsync(p => p.ProjectId == projectId);
        var workItem = await context.WorkItems.FirstOrDefaultAsync(w => w.WorkItemId == workItemId && w.ProjectId == projectId);
        
        if (project == null)
        {
            return Results.NotFound("Project not found");
        }

        if (workItem == null)
        {
            return Results.NotFound("Work item not found");
        }
        
        if (string.IsNullOrWhiteSpace(request.Title))
        {
            return Results.BadRequest();
        }
        
        workItem.Title = request.Title;
        await context.SaveChangesAsync();
        
        return Results.Json(workItem, statusCode: StatusCodes.Status200OK);
    });

app.Run();
