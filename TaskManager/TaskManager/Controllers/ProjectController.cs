using Contracts.DTOs;
using Contracts.Responses;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Persistence.Context;
using Persistence.Models;
using TaskManager.Services;

namespace TaskManager.Controllers;

[ApiController, Route("[Controller]")]

public class ProjectController : ControllerBase
{   
     private readonly TaskManagerContext _context;
     private readonly ProjectServices _management;

    public ProjectController(TaskManagerContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<Task<Project>>> CreateProject([FromBody] ProjectDTO dto)
    {
        var response = _management.CreateProject(dto);
        return Ok(response);
    }
    public ActionResult CreatePdroject([FromBody] ProjectDTO dto)
    {
        var project = new Project
        {
            ProjectName = dto.ProjectName,
            ProjectStartDate = dto.ProjectStartDate,
            ProjectEndDate = dto.ProjectEndDate,
            ProjectPriority = dto.ProjectPriority,
            ProjectStatus = Enum.Parse<ProjectStatus>(dto.ProjectStatus)
        };
        _context.Projects.Add(project);
        _context.SaveChanges();
        var response = new ProjectResponses()
        {
            ProjectId = project.ProjectId,
            ProjectName = project.ProjectName,
            ProjectStartDate = project.ProjectStartDate,
            ProjectEndDate = project.ProjectEndDate,
            ProjectPriority = project.ProjectPriority,
            ProjectStatus = project.ProjectStatus.ToString()
        };
        return Ok(response);
    }
    [HttpDelete]
    [Route("id/{id}")]
    public ActionResult DeleteProject([FromRoute] Guid id)
    {
        var project = _context.Projects.FirstOrDefault(x => x.ProjectId == id);
        if (project is null)
        {
            throw new Exception($"Project with ID {id} not found");
        }

        _context.Projects.Remove(project);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpPut]
    [Route("id/{id}/status/{status}")]
    public ActionResult EditProject([FromRoute] string status,[FromRoute]Guid id)
    {
        var project = _context.Projects.FirstOrDefault(x => x.ProjectId == id);
        if (project is null)
        {
            throw new Exception($"Project with ID {id} not found");
        }

        project.ProjectStatus = Enum.Parse<ProjectStatus>(status);
        _context.Projects.Update(project);
        _context.SaveChanges();
        return Ok(project);
    }

    [HttpGet]
    public ActionResult<List<Project>> GetAllProjects()
    {
        var result = _context.Projects.ToList();
        var response = new List<ProjectResponses>();
        foreach (var project in result)
        {
            var projectResponse = new ProjectResponses
            {
                ProjectId = project.ProjectId,
                ProjectName = project.ProjectName,
                ProjectStartDate = project.ProjectStartDate,
                ProjectEndDate = project.ProjectEndDate,
                ProjectPriority = project.ProjectPriority,
                ProjectStatus = project.ProjectStatus.ToString()
            };
            
            response.Add(projectResponse);
        }
        return Ok(response);
    }

}