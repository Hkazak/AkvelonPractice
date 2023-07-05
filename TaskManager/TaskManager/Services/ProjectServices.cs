using Contracts.DTOs;
using Contracts.Responses;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.CompilerServices;
using Persistence.Context;
using Persistence.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Services;

public class ProjectServices
{
    private readonly TaskManagerContext _context;

    public ProjectServices(TaskManagerContext context)
    {
        _context = context;
    }

    public async Task<ProjectResponses> CreateProjectAsync(ProjectDTO dto)
    {
        var project = new Project
        {
            ProjectName = dto.ProjectName,
            ProjectStartDate = dto.ProjectStartDate,
            ProjectEndDate = dto.ProjectEndDate,
            ProjectPriority = dto.ProjectPriority,
            ProjectStatus = Enum.Parse<ProjectStatus>(dto.ProjectStatus),
            UserId = dto.UserId
        };
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();
        var response = new ProjectResponses
        {
            ProjectId = project.ProjectId,
            ProjectName = project.ProjectName,
            ProjectStartDate = project.ProjectStartDate,
            ProjectEndDate = project.ProjectEndDate,
            ProjectPriority = project.ProjectPriority,
            ProjectStatus = project.ProjectStatus.ToString()
        };
        return response;
    }

    public async Task DeleteProjectAsync(Guid id)
    {
        var project = _context.Projects.FirstOrDefault(x => x.ProjectId == id);
        if (project is null)
        {
            throw new Exception($"Project with ID {id} not found");
        }

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();
    }

    public async Task<List<ProjectResponses>> GetAllProjectsAsync()
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
        return response;
    }

    public async Task<Project> EditProjectAsync(string status, Guid id)
    {
        var project = _context.Projects.FirstOrDefault(x => x.ProjectId == id);
        if (project is null)
        {
            throw new Exception($"Project with ID {id} not found");
        }

        project.ProjectStatus = Enum.Parse<ProjectStatus>(status);
        _context.Projects.Update(project);
        await _context.SaveChangesAsync();
        return project;
    }

}
