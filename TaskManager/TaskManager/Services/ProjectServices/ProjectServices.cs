using Contracts.DTOs;
using Contracts.Responses;
using Microsoft.AspNetCore.Http.HttpResults;
using Persistence.Context;
using Persistence.Models;

namespace TaskManager.Services;

public class ProjectServices
{
    private readonly TaskManagerContext _context;

    public ProjectServices(TaskManagerContext context)
    {
        _context = context;
    }

    public Task<Project> CreateProject(ProjectDTO dto)
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
        return ok;
    }
    
    
}
