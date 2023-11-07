using WorkItem.Task.Services.Contracts;

namespace WorkItem.Task.UnitOfWork
{
    public interface IUnitOfWork
    {
        IEntityRepository<WorkItem.Task.Models.Task> TaskRepository { get; set; }
        IEntityRepository<WorkItem.Task.Models.TaskStatus> TaskStatusRepository { get; set; }
        IEntityRepository<WorkItem.Task.Models.UserProfile> UserProfileRepository { get; set; }
        void Dispose();
        void Save();
    }
}
