using Contracts.DTOs;
using Contracts.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using TaskStatus = Persistence.Models.TaskStatus;

namespace TaskManager.Controllers;

[ApiController, Route("[Controller]")]

public class TasksController : ControllerBase
{
      private readonly TaskManagerContext _context;

    public TasksController(TaskManagerContext context)
    {
        _context = context;
    }

    [HttpPost]
    public ActionResult CreateTask([FromBody] TaskDTO dto)
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
            TaskStatus = Enum.Parse<Persistence.Models.TaskStatus>(dto.TaskStatus),
            Project = project
        };
        _context.Tasks.Add(task);
        _context.SaveChanges();
        var response = new TaskResponses()
        {
            TaskId = task.TaskId,
            TaskName = task.TaskName,
            TaskPriority = task.TaskPriority,
            TaskDescription = task.TaskDescription,
            TaskStatus = task.TaskStatus.ToString(),
            ProjectId = task.Project.ProjectId
        };
        return Ok(response);
    }

    [HttpDelete]
    [Route("id/{id}")]
    public ActionResult DeleteTask([FromRoute] Guid id)
    {
        var task = _context.Tasks.FirstOrDefault(x => x.TaskId == id);
        if (task is null)
        {
            throw new Exception($"Task with ID {id} not found");
        }

        _context.Tasks.Remove(task);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpPut] [Route("id/{id}/status/{status}")]
    public ActionResult EditTask ([FromRoute] string status,[FromRoute]Guid id)
    {
        var task = _context.Tasks.FirstOrDefault(x => x.TaskId == id);
        if (task is null)
        {
            throw new Exception($"Task with ID {id} not found");
        }

        task.TaskStatus = Enum.Parse<TaskStatus>(status);
        _context.Tasks.Update(task);
        _context.SaveChanges();
        return Ok(task);
    }

    [HttpGet] public ActionResult<List<TaskResponses>> GetAllTasks()
    {
        var result = _context.Tasks.Include(x => x.Project).ToList();
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
        return Ok(response);
    }
}