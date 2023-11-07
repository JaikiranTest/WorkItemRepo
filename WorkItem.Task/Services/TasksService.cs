using WorkItem.Task.Models;
using WorkItem.Task.Services.Contracts;
using WorkItem.Task.UnitOfWork;

namespace WorkItem.Task.Services
{
    public class TasksService : ITasksService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TasksService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> CreateTaskAsync(Models.Task task)
        {
            return await _unitOfWork.TaskRepository.CreateAsync(task);
        }

        public async Task<bool> DeleteTaskAsync(Models.Task task)
        {
            return await _unitOfWork.TaskRepository.DeleteAsync(task);
        }

        public async Task<IEnumerable<Models.Task>> GetTasksAsync()
        {
            return await _unitOfWork.TaskRepository.GetAllAsync();
        }

        public async Task<Models.Task?> GetTaskByIDAsync(int? id)
        {
            return await _unitOfWork.TaskRepository.GetByIDAsync(id);
        }

        public async Task<bool> UpdateTaskAsync(Models.Task task)
        {
            return await _unitOfWork.TaskRepository.UpdateAsync(task);
        }

        public async Task<IEnumerable<string?>> GetEmailIDsFromUserProfileAsync()
        {
            var result = await _unitOfWork.UserProfileRepository.GetAllAsync();
            return result.Select(x => x.EmailId).ToList();
        }

        public async Task<IEnumerable<UserProfile?>> GetUserProfilesAsync()
        {
            return await _unitOfWork.UserProfileRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Models.TaskStatus?>> GetTaskStatusAsync()
        {
            return await _unitOfWork.TaskStatusRepository.GetAllAsync();
        }
    }
}
