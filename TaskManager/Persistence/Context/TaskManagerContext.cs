using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Persistence.Models;

namespace Persistence.Context
{
    public class TaskManagerContext : DbContext
    {
        public DbSet<Models.Project> Projects { get; init; }
        public DbSet<Models.Task> Tasks { get; init; }
        public DbSet<Models.User> Users { get; init; }

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
}