using DataAccessLab.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLab.Api.Data;

public class DataAccessDbContext: DbContext
{
    public DbSet<Project> Projects { get; set; }
    public DbSet<WorkItem> WorkItems { get; set; }
    
    public DataAccessDbContext(DbContextOptions<DataAccessDbContext> options) : base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WorkItem>()
            .HasOne<Project>()
            .WithMany(p => p.WorkItems)
            .HasForeignKey(w => w.ProjectId);
    }
}