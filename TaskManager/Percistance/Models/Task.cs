using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Percistance.Models;

public class Task
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TaskId { get; set; }
    public string TaskName { get; set; }
    public string TaskDescription { get; set; }
    public int Priority { get; set; }
    public TaskStatus Status { get; set; }
    public DateOnly DeadLine { get; set; }
    public Project Project { get; set; }
    [ForeignKey(nameof(Models.Project))]
    public Guid ProjectId { get; set; }
}   