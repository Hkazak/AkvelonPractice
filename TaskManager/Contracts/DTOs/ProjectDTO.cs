namespace Contracts.DTOs;

public record ProjectDTO( string ProjectName, DateTime ProjectStartDate, DateTime ProjectEndDate, int ProjectPriority, string ProjectStatus, Guid UserId);