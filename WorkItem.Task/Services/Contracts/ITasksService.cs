namespace WorkItem.Task.Services.Contracts
{
    public interface ITasksService
    {
        public Task<IEnumerable<Models.Task>> GetTasksAsync();
        public Task<Models.Task?> GetTaskByIDAsync(int? id);
        public Task<bool> CreateTaskAsync(Models.Task task);
        public Task<bool> UpdateTaskAsync(Models.Task task);
        public Task<bool> DeleteTaskAsync(Models.Task task);
        public Task<IEnumerable<string?>> GetEmailIDsFromUserProfileAsync();
        public Task<IEnumerable<Models.UserProfile?>> GetUserProfilesAsync();
        public Task<IEnumerable<Models.TaskStatus?>> GetTaskStatusAsync();
    }
}
