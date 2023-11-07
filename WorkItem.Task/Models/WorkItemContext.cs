using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace WorkItem.Task.Models
{
    [ExcludeFromCodeCoverage]
    public class WorkItemContext : DbContext
    {
        public WorkItemContext(DbContextOptions<WorkItemContext> options) : base(options)
        {
            Database.EnsureCreated();
            Database.Migrate();
        }

        public virtual DbSet<Task> Tasks { get; set; }

        public virtual DbSet<TaskStatus> TaskStatuses { get; set; }

        public virtual DbSet<UserProfile> UserProfiles { get; set; }
    }
}
