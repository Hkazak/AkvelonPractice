using Contracts.DTOs;
using Contracts.Responses;
using Microsoft.AspNetCore.Mvc;
using Persistence.Context;
using Persistence.Models;
using TaskManager.Services;

namespace TaskManager.Controllers;

[ApiController, Route("[Controller]")]

public class ProjectController : ControllerBase
{   
     private readonly TaskManagerContext _context;
     private readonly ProjectServices _projectServices;

     public ProjectController(TaskManagerContext context, ProjectServices projectServices)
     {
         _context = context;
         _projectServices = projectServices;
     }
     
     [HttpPost]
    public async Task<ActionResult> CreateProject([FromBody] ProjectDTO dto)
    {
        var response = await _projectServices.CreateProjectAsync(dto);
        return Ok(response);
    }
    [HttpDelete]
    [Route("id/{id}")]
    public async Task<ActionResult> DeleteProject([FromRoute] Guid id)
    {
        await _projectServices.DeleteProjectAsync(id);
        return NoContent();
    }

    [HttpPut]
    [Route("id/{id}/status/{status}")]
    public async Task<ActionResult> EditProject([FromRoute] string status,[FromRoute]Guid id)
    {
        var project = await _projectServices.EditProjectAsync(status, id);
        return Ok(project);
    }

    [HttpGet]
    public async Task<ActionResult<List<ProjectResponses>>> GetAllProjects()
    {
        var response = await _projectServices.GetAllProjectsAsync();
        return Ok(response);
    }

}