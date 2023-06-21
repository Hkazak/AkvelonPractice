namespace Contracts.DTOs;

public class ProjectDTO
{
    public string ProjectName { get; set; }
    public DateTime ProjectStartDate { get; set; }
    public DateTime ProjectEndDate { get; set; }
    public int ProjectPriority { get; set; }
    public string ProjectStatus { get; set; }
}