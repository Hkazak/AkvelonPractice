using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Percistance.Models;
using DbContext = System.Data.Entity.DbContext;
using Task = Percistance.Models.Task;

namespace Percistance.Context;

public class TaskManagerContext : DbContext
{
    public DbSet<Project> Projects { get; set; }
    public DbSet<Task> Tasks { get; set; }

    protected TaskManagerContext()
    {
    }
    
    public TaskManagerContext(DbContextOptions options) : base(options)
    {
    }

    public async Task<int> SaveChangesAsync()
    {
        return await base.SaveChangesAsync();
    }
}