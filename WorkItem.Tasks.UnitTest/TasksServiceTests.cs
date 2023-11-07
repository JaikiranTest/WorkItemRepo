using Moq;
using System.Diagnostics.CodeAnalysis;
using WorkItem.Task.Services;
using WorkItem.Task.Services.Contracts;
using WorkItem.Task.UnitOfWork;

namespace WorkItem.Tasks.UnitTest
{
    [ExcludeFromCodeCoverage]
    public class TasksServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockIUnitOfWork;
        private readonly ITasksService _tasksService;
        List<Task.Models.Task> _tasks;
        List<Task.Models.TaskStatus> _tasksStatus;
        List<Task.Models.UserProfile> _userProfiles;

        public TasksServiceTests()
        {
            _mockIUnitOfWork = new Mock<IUnitOfWork>();

            _tasksService = new TasksService(_mockIUnitOfWork.Object);

            _tasks = new List<Task.Models.Task>()
            {
                new Task.Models.Task { Id = 1, Title = "Test-1", Assigned = "kiran@gmail.com", StatusId = 1, Discription = "Discription-1", Comments = "Comments-1", Status = new WorkItem.Task.Models.TaskStatus{ Id = 1, Status = "To Do"} },
                new Task.Models.Task { Id = 2, Title = "Test-2", Assigned = "Shwetha@gmail.com", StatusId = 2, Discription = "Discription-2", Comments = "Comments-2" },
                new Task.Models.Task { Id = 3, Title = "Test-3", Assigned = "Rama@gmail.com", StatusId = 3, Discription = "Discription-3", Comments = "Comments-3", Status = new WorkItem.Task.Models.TaskStatus{ Id = 3, Status = "Done"} },
                new Task.Models.Task { Id = 4, Title = "Test-4", Assigned = "John@gmail.com", StatusId = 2, Discription = "Discription-4", Comments = "Comments-4", Status = new WorkItem.Task.Models.TaskStatus{ Id = 2, Status = "Doing"} },
                new Task.Models.Task { Id = 5, Title = "Test-5", Assigned = "Mohammed@gmail.com", StatusId = 3, Discription = "Discription-5", Comments = "Comments-5", Status = new WorkItem.Task.Models.TaskStatus{ Id = 3, Status = "Done"} }
            };

            _tasksStatus = new List<Task.Models.TaskStatus>
            {
                new Task.Models.TaskStatus{ Id = 1, Status = "To Do" },
                new Task.Models.TaskStatus{ Id = 2, Status = "Doing" },
                new Task.Models.TaskStatus{ Id = 3, Status = "Done" }
            };

            _userProfiles = new List<WorkItem.Task.Models.UserProfile>
            {
                new WorkItem.Task.Models.UserProfile{ Id = 1, UserName = "Kiran", EmailId = "Kiran@gmail.com" },
                new WorkItem.Task.Models.UserProfile{ Id = 2, UserName = "Shwetha", EmailId = "Shwetha@gmail.com" },
                new WorkItem.Task.Models.UserProfile{ Id = 3, UserName = "Rama", EmailId = "Rama@gmail.com" }
            };
        }

        #region CreateTaskAsync_Executes_ReturnBoolean
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async void CreateTaskAsync_Executes_ReturnBoolean(bool returnValue)
        {
            var task = _tasks.First();

            _mockIUnitOfWork.Setup(repo => repo.TaskRepository.CreateAsync(It.IsAny<Task.Models.Task>())).ReturnsAsync(returnValue);

            var result = await _tasksService.CreateTaskAsync(task);

            Assert.IsType<bool>(result);

            Assert.Equal(returnValue, result);
        }
        #endregion

        #region DeleteTaskAsync_Executes_ReturnBoolean
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async void DeleteTaskAsync_Executes_ReturnBoolean(bool returnValue)
        {
            var task = _tasks.First();

            _mockIUnitOfWork.Setup(repo => repo.TaskRepository.DeleteAsync(It.IsAny<Task.Models.Task>())).ReturnsAsync(returnValue);

            var result = await _tasksService.DeleteTaskAsync(task);

            Assert.IsType<bool>(result);

            Assert.Equal(returnValue, result);
        }
        #endregion

        #region GetTaskAsync_Executes_ReturnTaskList
        [Fact]
        public async void GetTaskAsync_Executes_ReturnTaskList()
        {
            _mockIUnitOfWork.Setup(repo => repo.TaskRepository.GetAllAsync()).ReturnsAsync(_tasks);

            var result = await _tasksService.GetTasksAsync();

            Assert.IsAssignableFrom<List<Task.Models.Task>>(result);

            Assert.Equal(_tasks, result);
        }
        #endregion

        #region GetTaskByIDAsync_Executes_ReturnTask
        [Fact]
        public async void GetTaskByIDAsync_Executes_ReturnTask()
        {
            var task = _tasks.First();

            _mockIUnitOfWork.Setup(repo => repo.TaskRepository.GetByIDAsync(It.IsAny<int>())).ReturnsAsync(task);

            var result = await _tasksService.GetTaskByIDAsync(task.Id);

            Assert.IsAssignableFrom<Task.Models.Task>(result);

            Assert.Equal(task, result);
        }
        #endregion

        #region UpdateTaskByIDAsync_Executes_ReturnBoolean
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async void UpdateTaskByIDAsync_Executes_ReturnBoolean(bool resultValue)
        {
            var task = _tasks.First();

            _mockIUnitOfWork.Setup(repo => repo.TaskRepository.UpdateAsync(It.IsAny<Task.Models.Task>())).ReturnsAsync(resultValue);

            var result = await _tasksService.UpdateTaskAsync(task);

            Assert.IsType<bool>(result);

            Assert.Equal(resultValue, result);
        }
        #endregion

        #region GetEmailIDsFromUserProfileAsync_Executes_ReturnUserProfileEmails
        [Fact]
        public async void GetEmailIDsFromUserProfileAsync_Executes_ReturnUserProfileEmails()
        {
            var emailList = _userProfiles.Select(x => x.EmailId).ToList();

            _mockIUnitOfWork.Setup(repo => repo.UserProfileRepository.GetAllAsync()).ReturnsAsync(_userProfiles);

            var result = await _tasksService.GetEmailIDsFromUserProfileAsync();

            Assert.IsAssignableFrom<List<string>>(result);

            Assert.Equal(emailList, result);
        }
        #endregion

        #region GetUserProfilesAsync_Executes_ReturnUserProfiles
        [Fact]
        public async void GetUserProfilesAsync_Executes_ReturnUserProfiles()
        {
            _mockIUnitOfWork.Setup(repo => repo.UserProfileRepository.GetAllAsync()).ReturnsAsync(_userProfiles);

            var result = await _tasksService.GetUserProfilesAsync();

            Assert.IsAssignableFrom<List<Task.Models.UserProfile>>(result);

            Assert.Equal(_userProfiles, result);
        }
        #endregion

        #region GetTaskStatusAsync_Executes_ReturnTaskStatusList
        [Fact]
        public async void GetTaskStatusAsync_Executes_ReturnTaskStatusList()
        {
            _mockIUnitOfWork.Setup(repo => repo.TaskStatusRepository.GetAllAsync()).ReturnsAsync(_tasksStatus);

            var result = await _tasksService.GetTaskStatusAsync();

            Assert.IsAssignableFrom<List<Task.Models.TaskStatus>>(result);

            Assert.Equal(_tasksStatus, result);
        }
        #endregion
    }
}
