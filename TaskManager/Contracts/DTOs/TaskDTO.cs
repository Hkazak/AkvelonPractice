namespace Contracts.DTOs;

public record TaskDTO (Guid TaskId,string TaskName, string TaskDescription, int TaskPriority, string TaskStatus, DateTime TaskDeadline, Guid ProjectId);