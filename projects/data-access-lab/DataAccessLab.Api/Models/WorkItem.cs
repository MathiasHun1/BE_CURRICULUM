namespace DataAccessLab.Api.Models;

public class WorkItem
{
    public int WorkItemId { get; set; }
    public string Title { get; set; } = String.Empty;
    public int ProjectId { get; set; }
}