using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Diagnostics.CodeAnalysis;
using WorkItem.Task.Controllers;
using WorkItem.Task.Services.Contracts;

namespace WorkItem.Tasks.UnitTest
{
    [ExcludeFromCodeCoverage]
    public class TasksControllerTests
    {
        private readonly Mock<ITasksService> _mockTasksRepository;
        private readonly TasksController _tasksController;
        private List<Task.Models.Task> _tasks;

        public TasksControllerTests()
        {
            _mockTasksRepository = new Mock<ITasksService>();

            _tasksController = new TasksController(_mockTasksRepository.Object);

            _tasks = new List<Task.Models.Task>()
            {
                new Task.Models.Task { Id = 1, Title = "Test-1", Assigned = "kiran@gmail.com", StatusId = 1, Discription = "Discription-1", Comments = "Comments-1", Status = new WorkItem.Task.Models.TaskStatus{ Id = 1, Status = "To Do"} },
                new Task.Models.Task { Id = 2, Title = "Test-2", Assigned = "Shwetha@gmail.com", StatusId = 2, Discription = "Discription-2", Comments = "Comments-2" },
                new Task.Models.Task { Id = 3, Title = "Test-3", Assigned = "Rama@gmail.com", StatusId = 3, Discription = "Discription-3", Comments = "Comments-3", Status = new WorkItem.Task.Models.TaskStatus{ Id = 3, Status = "Done"} },
                new Task.Models.Task { Id = 4, Title = "Test-4", Assigned = "John@gmail.com", StatusId = 2, Discription = "Discription-4", Comments = "Comments-4", Status = new WorkItem.Task.Models.TaskStatus{ Id = 2, Status = "Doing"} },
                new Task.Models.Task { Id = 5, Title = "Test-5", Assigned = "Mohammed@gmail.com", StatusId = 3, Discription = "Discription-5", Comments = "Comments-5", Status = new WorkItem.Task.Models.TaskStatus{ Id = 3, Status = "Done"} }
            };
        }

        #region Index_ActionExecutes_ReturnView
        [Fact]
        public async void Index_ActionExecutes_ReturnView()
        {
            var result = await _tasksController.Index();

            Assert.IsType<ViewResult>(result);
        }
        #endregion

        #region Index_ActionExecutes_ReturnTasksList
        [Fact]
        public async void Index_ActionExecutes_ReturnTasksList()
        {
            _mockTasksRepository.Setup(repo => repo.GetTasksAsync()).ReturnsAsync(_tasks);

            var result = await _tasksController.Index();

            var viewResult = Assert.IsType<ViewResult>(result);

            var taskList = Assert.IsAssignableFrom<IEnumerable<Task.Models.Task>>(viewResult.Model);

            Assert.Equal<int>(5, taskList.Count());
        }
        #endregion

        #region Details_IdInValid_ReturnNotFound
        [Fact]
        public async void Details_IdInValid_ReturnNotFound()
        {
            Task.Models.Task task = null;

            _mockTasksRepository.Setup(repo => repo.GetTaskByIDAsync(0)).ReturnsAsync(task);

            var result = await _tasksController.Details(0);

            var notFound = Assert.IsType<NotFoundResult>(result);

            Assert.Equal<int>(404, notFound.StatusCode);
        }
        #endregion

        #region Details_ValidId_ReturnTask
        [Theory]
        [InlineData(1)]
        public async void Details_ValidId_ReturnTask(int taskId)
        {
            Task.Models.Task? task = _tasks.FirstOrDefault(x => x.Id == taskId);

            _mockTasksRepository.Setup(repo => repo.GetTaskByIDAsync(taskId)).ReturnsAsync(task);

            var result = await _tasksController.Details(taskId);

            var viewResult = Assert.IsType<ViewResult>(result);

            var resultTask = Assert.IsAssignableFrom<Task.Models.Task>(viewResult.Model);

            Assert.Equal(task?.Id, resultTask.Id);
            Assert.Equal(task?.Title, resultTask.Title);
            Assert.Equal(task?.Assigned, resultTask.Assigned);
            Assert.Equal(task?.Discription, resultTask.Discription);
            Assert.Equal(task?.StatusId, resultTask.StatusId);
            Assert.Equal(task?.Comments, resultTask.Comments);
        }
        #endregion

        #region Create_ActionExecutes_ReturnView
        [Fact]
        public async void Create_ActionExecutes_ReturnView()
        {
            var result = await _tasksController.Create();

            Assert.IsType<ViewResult>(result);
        }
        #endregion

        #region CreatePost_InValidModelState_ReturnView
        [Fact]
        public async void CreatePost_InValidModelState_ReturnView()
        {
            _tasksController.ModelState.AddModelError("Assigned", "The 'Assigned To' field is required.");

            var result = await _tasksController.Create(_tasks.First());

            var viewResult = Assert.IsType<ViewResult>(result);

            Assert.IsType<Task.Models.Task>(viewResult.Model);
        }
        #endregion

        #region CreatePost_ValidModelSate_ReturnRedirectToIndexAction
        [Fact]
        public async void CreatePost_ValidModelSate_ReturnRedirectToIndexAction()
        {
            var result = await _tasksController.Create(_tasks.First());

            var redirect = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Index", redirect.ActionName);
        }
        #endregion

        #region CreatePost_ValidModelState_CreateMethodExecute
        [Fact]
        public async void CreatePost_ValidModelState_CreateMethodExecute()
        {
            Task.Models.Task newTask = null;

            _mockTasksRepository.Setup(repo => repo.CreateTaskAsync(It.IsAny<Task.Models.Task>()))
                                  .Callback<Task.Models.Task>(x => newTask = x);

            var result = await _tasksController.Create(_tasks.First());

            _mockTasksRepository.Verify(repo => repo.CreateTaskAsync(It.IsAny<Task.Models.Task>()), Times.Once);

            Assert.Equal(_tasks.First().Id, newTask.Id);
        }
        #endregion

        #region CreatePost_InValidModelState_NeverCreateExecute
        [Fact]
        public async void CreatePost_InValidModelState_NeverCreateExecute()
        {
            _tasksController.ModelState.AddModelError("Assigned", "");

            var result = await _tasksController.Create(_tasks.First());

            _mockTasksRepository.Verify(repo => repo.CreateTaskAsync(It.IsAny<Task.Models.Task>()), Times.Never);
        }
        #endregion

        #region Edit_IdIsNull_ReturnNotFound
        [Fact]
        public async void Edit_IdIsNull_ReturnNotFound()
        {
            var result = await _tasksController.Edit(null);

            var notFound = Assert.IsType<NotFoundResult>(result);

            Assert.Equal<int>(404, notFound.StatusCode);
        }
        #endregion

        #region Edit_IdInValid_ReturnNotFound
        [Theory]
        [InlineData(9)]
        public async void Edit_IdInValid_ReturnNotFound(int taskId)
        {
            Task.Models.Task task = null;

            _mockTasksRepository.Setup(repo => repo.GetTaskByIDAsync(taskId)).ReturnsAsync(task);

            var result = await _tasksController.Edit(taskId);

            var notFound = Assert.IsType<NotFoundResult>(result);

            Assert.Equal<int>(404, notFound.StatusCode);
        }
        #endregion

        #region Edit_ActionExecutes_ReturnTask
        [Theory]
        [InlineData(2)]
        public async void Edit_ActionExecutes_ReturnTask(int taskId)
        {
            var task = _tasks.First(x => x.Id == taskId);

            _mockTasksRepository.Setup(repo => repo.GetTaskByIDAsync(taskId)).ReturnsAsync(task);

            var result = await _tasksController.Edit(taskId);

            var viewResult = Assert.IsType<ViewResult>(result);

            var resultTask = Assert.IsAssignableFrom<Task.Models.Task>(viewResult.Model);

            Assert.Equal(task?.Id, resultTask.Id);
            Assert.Equal(task?.Title, resultTask.Title);
            Assert.Equal(task?.Assigned, resultTask.Assigned);
            Assert.Equal(task?.Discription, resultTask.Discription);
            Assert.Equal(task?.StatusId, resultTask.StatusId);
            Assert.Equal(task?.Comments, resultTask.Comments);
        }
        #endregion

        #region EditPost_InValidModelState_ReturnView
        [Theory]
        [InlineData(2)]
        public async void EditPost_InValidModelState_ReturnView(int taskId)
        {
            _tasksController.ModelState.AddModelError("Assigned", " ");

            var result = await _tasksController.Edit(taskId, _tasks.First(x => x.Id == taskId));

            var viewResult = Assert.IsType<ViewResult>(result);

            Assert.IsType<Task.Models.Task>(viewResult.Model);
        }
        #endregion

        #region EditPost_ValidModelState_ReturnRedirectToIndexAction
        [Theory]
        [InlineData(1)]
        public async void EditPost_ValidModelState_ReturnRedirectToIndexAction(int taskId)
        {
            var task = _tasks.First(x => x.Id == taskId);

            _mockTasksRepository.Setup(repo => repo.UpdateTaskAsync(task)).ReturnsAsync(true);

            var result = await _tasksController.Edit(taskId, _tasks.First(x => x.Id == taskId));

            var redirect = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Index", redirect.ActionName);
        }
        #endregion

        #region EditPost_ValidModelState_UpdateMethodExecute
        [Theory]
        [InlineData(1)]
        public async void EditPost_ValidModelState_UpdateMethodExecute(int taskId)
        {
            var task = _tasks.First(x => x.Id == taskId);

            _mockTasksRepository.Setup(repo => repo.UpdateTaskAsync(task));

            await _tasksController.Edit(taskId, task);

            _mockTasksRepository.Verify(repo => repo.UpdateTaskAsync(It.IsAny<Task.Models.Task>()), Times.Once);
        }
        #endregion

        #region Delete_IdIsNull_ReturnNotFound
        [Fact]
        public async void Delete_IdIsNull_ReturnNotFound()
        {
            var result = await _tasksController.Delete(null);

            Assert.IsType<NotFoundResult>(result);
        }
        #endregion

        #region Delete_IdIsNotEqualTask_ReturnNotFound
        [Theory]
        [InlineData(0)]
        public async void Delete_IdIsNotEqualTask_ReturnNotFound(int taskId)
        {
            Task.Models.Task task = null;
            _mockTasksRepository.Setup(repo => repo.GetTaskByIDAsync(taskId)).ReturnsAsync(task);

            var result = await _tasksController.Delete(taskId);

            Assert.IsType<NotFoundResult>(result);
        }
        #endregion

        #region Delete_ActionExecutes_ReturnList
        [Theory]
        [InlineData(1)]
        public async void Delete_ActionExecutes_ReturnList(int taskId)
        {
            var task = _tasks.First(x => x.Id == taskId);

            _mockTasksRepository.Setup(repo => repo.GetTaskByIDAsync(taskId)).ReturnsAsync(task);

            var result = await _tasksController.Delete(taskId);

            var viewResult = Assert.IsType<ViewResult>(result);

            Assert.IsAssignableFrom<Task.Models.Task>(viewResult.Model);
        }
        #endregion

        #region DeleteConfirm_ActionExecutes_ReturnRedirectToIndexAction
        [Theory]
        [InlineData(1)]
        public async void DeleteConfirmed_ActionExecutes_ReturnRedirectToIndexAction(int taskId)
        {
            var task = _tasks.First(x => x.Id == taskId);

            _mockTasksRepository.Setup(repo => repo.GetTaskByIDAsync(taskId)).ReturnsAsync(_tasks.First(x => x.Id == taskId));

            _mockTasksRepository.Setup(repo => repo.DeleteTaskAsync(task)).ReturnsAsync(true);

            var result = await _tasksController.DeleteConfirmed(taskId);

            Assert.IsType<RedirectToActionResult>(result);
        }
        #endregion

        #region DeleteConfirmed_ActionExecutes_DeleteMethodExecute
        [Theory]
        [InlineData(1)]
        public async void DeleteConfirmed_ActionExecutes_DeleteMethodExecute(int taskId)
        {
            Task.Models.Task task = _tasks.First(x => x.Id == taskId);

            _mockTasksRepository.Setup(repo => repo.GetTaskByIDAsync(task.Id)).ReturnsAsync(task);

            await _tasksController.DeleteConfirmed(taskId);

            _mockTasksRepository.Verify(repo => repo.DeleteTaskAsync(It.IsAny<Task.Models.Task>()), Times.Once);
        }
        #endregion
    }
}
