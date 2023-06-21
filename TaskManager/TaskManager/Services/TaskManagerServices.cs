using Contracts.DTOs;
using Percistance.Context;
using Percistance.Models;

namespace TaskManager.Services;

public class TaskManagerServices
{
    private readonly TaskManagerContext _context;

    public TaskManagerServices(TaskManagerContext context)
    {
        _context = context;
    }

    public async Task<Project> CreateProject(ProjectDTO dto)
    {
        if (string.IsNullOrEmpty(dto.ProjectName))
        {
            throw new NullReferenceException();
        }

        var newProject = new Project
        {
            ProjectId = Guid.NewGuid(),
            ProjectName = dto.ProjectName
        };
        await _context.Projects.AddAsync(newProject);
        await _context.SaveChangesAsync();
        return newProject;
    }
    
    
}
