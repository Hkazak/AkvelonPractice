using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persistence.Models;

public class Project
{
    [Key,  DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid ProjectId { get; init; }
    public string ProjectName { get;set; }
    public DateTime ProjectStartDate { get; set; }
    public DateTime ProjectEndDate { get; set; }
    public int ProjectPriority { get; set; }
    public ProjectStatus ProjectStatus { get; set; }
    public ICollection<Task> Tasks { get; init; } = new List<Task>();
    [ForeignKey(nameof(Models.User.UserId))]
    public Guid UserId { get; init; }
}