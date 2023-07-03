using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persistence.Models;

public record Task
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid TaskId { get; init; }
    public string TaskName { get; set; }
    public string TaskDescription { get; set; }
    public int TaskPriority { get; set; }
    public TaskStatus TaskStatus { get; set; }
    public DateTime TaskDeadline { get; set; }
    public Project Project { get; set; }
    [ForeignKey(nameof(Models.Project.ProjectId))]
    public Guid ProjectId { get; init; }
}   