using Contracts.DTOs;
using Contracts.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using TaskManager.Services;
using TaskStatus = Persistence.Models.TaskStatus;

namespace TaskManager.Controllers;

[ApiController, Route("[Controller]")]

public class TasksController : ControllerBase
{
      private readonly TaskManagerContext _context;
      private readonly TaskService _taskService;

    public TasksController(TaskManagerContext context, TaskService taskService)
    {
        _context = context;
        _taskService = taskService;
    }

    [HttpPost]
    public async Task<ActionResult> CreateTask([FromBody] TaskDTO dto)
    {
        var response =await _taskService.CreateTaskAsync(dto);
        return Ok(response);
    }

    [HttpDelete]
    [Route("id/{id}")]
    public async Task<ActionResult> DeleteTask([FromRoute] Guid id)
    {
        await _taskService.DeleteTaskAsync(id);
        return NoContent();
    }

    [HttpPut] [Route("id/{id}/status/{status}")]
    public async Task<ActionResult> EditTask ([FromRoute] string status,[FromRoute]Guid id)
    {
        var task =await _taskService.EditTaskAsync(status, id);
        return Ok(task);
    }

    [HttpGet] public async Task<ActionResult<List<TaskResponses>>> GetAllTasks()
    {
        var result =await _taskService.GetAllTasksAsync();
        return Ok(result);
    }
}