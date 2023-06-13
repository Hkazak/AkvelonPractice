namespace Percistance.Models;

public class Task
{
    public int TaskId { get; set; }
    public string TaskName { get; set; }
    public string TaskDescription { get; set; }
    public int Priority { get; set; }
    public TaskStatus Status { get; set; }
    public DateOnly DeadLine { get; set; }
    public User UserId { get; set; }
}   