namespace DataAccessLab.Api.Models;

public class Project
{
    public int  ProjectId { get; set; }
    public string Name { get; set; } = String.Empty;
    public ICollection<WorkItem> WorkItems { get; set; } = new List<WorkItem>();
}