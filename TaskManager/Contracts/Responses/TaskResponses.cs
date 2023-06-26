namespace Contracts.Responses;

public class TaskResponses
{
    public Guid TaskId { get; set; }
    public string TaskName { get; set; }
    public string TaskDescription { get; set; }
    public int TaskPriority { get; set; }
    public string TaskStatus { get; set; }
    public Guid ProjectId { get; set; }
}