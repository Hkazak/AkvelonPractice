using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Percistance.Models;

public class Project
{
    [Key,  DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid ProjectId { get; set; }
    public string ProjectName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Priority { get; set; }
    public ProjectStatus Status { get; set; }

    public ICollection<Task> Tasks = new List<Task>();
}