using Contracts.DTOs;
using Contracts.Responses;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Task = Persistence.Models.Task;
using TaskStatus = Persistence.Models.TaskStatus;

namespace TaskManager.Services;

public class TaskService
{
    private readonly TaskManagerContext _context;

    public TaskService(TaskManagerContext context)
    {
        _context = context;
    }

    public async Task<TaskResponses> CreateTaskAsync(TaskDTO dto)
    {
        var project = _context.Projects.FirstOrDefault(x => x.ProjectId == dto.ProjectId);
        if (project is null)
        {
            throw new Exception($"Project with ID {dto.ProjectId} not found");
        }

        var task = new Persistence.Models.Task()
        {
            TaskDescription = dto.TaskDescription,
            TaskPriority = dto.TaskPriority,
            TaskName = dto.TaskName,
            TaskDeadline = dto.TaskDeadline,
            TaskStatus = Enum.Parse<Persistence.Models.TaskStatus>(dto.TaskStatus),
            Project = project
        };
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
        var response = new TaskResponses()
        {
            TaskId = task.TaskId,
            TaskName = task.TaskName,
            TaskPriority = task.TaskPriority,
            TaskDescription = task.TaskDescription,
            TaskStatus = task.TaskStatus.ToString(),
            ProjectId = task.Project.ProjectId
        };
        return response;
    }

    public async System.Threading.Tasks.Task DeleteTaskAsync(Guid id)
    {
        var task = _context.Tasks.FirstOrDefault(x => x.TaskId == id);
        if (task is null)
        {
            throw new Exception($"Task with ID {id} not found");
        }

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
    }
       
    public async Task<Task> EditTaskAsync(string status, Guid id )
    {
        var task = _context.Tasks.FirstOrDefault(x => x.TaskId == id);
        if (task is null)
        {
            throw new Exception($"Task with ID {id} not found");
        }

        task.TaskStatus = Enum.Parse<TaskStatus>(status);
        _context.Tasks.Update(task);
        await _context.SaveChangesAsync();
        return task;
    }
    
    public async Task<List<TaskResponses>> GetAllTasksAsync()
    {
        var result =await _context.Tasks.Include(x => x.Project).ToListAsync();
        var response = new List<TaskResponses>(); 
        foreach (var task in result)
        {
            var taskResponse = new TaskResponses
            {
                TaskId = task.TaskId,
                TaskName = task.TaskName,
                TaskPriority = task.TaskPriority,
                TaskDescription = task.TaskDescription,
                TaskStatus = task.TaskStatus.ToString(),
                ProjectId = task.Project.ProjectId
            };
            
            response.Add(taskResponse);
        }
        return response;
    }
 
}
