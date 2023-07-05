namespace Contracts.DTOs;

public record TaskDTO (string TaskName, string TaskDescription, int TaskPriority, string TaskStatus, DateTime TaskDeadline, Guid ProjectId);