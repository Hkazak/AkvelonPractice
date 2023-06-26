namespace Contracts.DTOs;

public record ProjectDTO(Guid ProjectId, string ProjectName, DateTime ProjectStartDate, DateTime ProjectEndDate, int ProjectPriority, string ProjectStatus, Guid UserId);