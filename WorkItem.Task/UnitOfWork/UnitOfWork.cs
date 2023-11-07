using System.Diagnostics.CodeAnalysis;
using WorkItem.Task.Models;
using WorkItem.Task.Services;
using WorkItem.Task.Services.Contracts;

namespace WorkItem.Task.UnitOfWork
{
    [ExcludeFromCodeCoverage]
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        WorkItemContext _context;
        public IEntityRepository<WorkItem.Task.Models.Task> TaskRepository { get; set; }
        public IEntityRepository<WorkItem.Task.Models.TaskStatus> TaskStatusRepository { get; set; }
        public IEntityRepository<WorkItem.Task.Models.UserProfile> UserProfileRepository { get; set; }

        public UnitOfWork(WorkItemContext context)
        {
            _context = context;
            TaskRepository = new EntityRepository<WorkItem.Task.Models.Task>(_context);
            TaskStatusRepository = new EntityRepository<WorkItem.Task.Models.TaskStatus>(_context);
            UserProfileRepository = new EntityRepository<WorkItem.Task.Models.UserProfile>(_context);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
