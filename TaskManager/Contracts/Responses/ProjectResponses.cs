namespace Contracts.Responses;

public class ProjectResponses
{
    public Guid ProjectId { get; set; }
    public string ProjectName { get; set; } = null!;
    public DateTime ProjectStartDate { get; set; }
    public DateTime ProjectEndDate { get; set; }
    public int ProjectPriority { get; set; }
    public string ProjectStatus { get; set; } = null!;
}