using System.Diagnostics.CodeAnalysis;
using WorkItem.Task.Models;
using WorkItem.Task.Services;
using MoqGeneric = Telerik.JustMock;

namespace WorkItem.Tasks.UnitTest
{
    [ExcludeFromCodeCoverage]
    public class EntityRepositoryTests
    {
        List<Task.Models.Task> _tasks;

        public EntityRepositoryTests()
        {
            _tasks = new List<Task.Models.Task>()
            {
                new Task.Models.Task { Id = 1, Title = "Test-1", Assigned = "kiran@gmail.com", StatusId = 1, Discription = "Discription-1", Comments = "Comments-1", Status = new WorkItem.Task.Models.TaskStatus{ Id = 1, Status = "To Do"} },
                new Task.Models.Task { Id = 2, Title = "Test-2", Assigned = "Shwetha@gmail.com", StatusId = 2, Discription = "Discription-2", Comments = "Comments-2" },
                new Task.Models.Task { Id = 3, Title = "Test-3", Assigned = "Rama@gmail.com", StatusId = 3, Discription = "Discription-3", Comments = "Comments-3", Status = new WorkItem.Task.Models.TaskStatus{ Id = 3, Status = "Done"} },
                new Task.Models.Task { Id = 4, Title = "Test-4", Assigned = "John@gmail.com", StatusId = 2, Discription = "Discription-4", Comments = "Comments-4", Status = new WorkItem.Task.Models.TaskStatus{ Id = 2, Status = "Doing"} },
                new Task.Models.Task { Id = 5, Title = "Test-5", Assigned = "Mohammed@gmail.com", StatusId = 3, Discription = "Discription-5", Comments = "Comments-5", Status = new WorkItem.Task.Models.TaskStatus{ Id = 3, Status = "Done"} }
            };
        }
        
        #region CreateTaskAsync_Executes_ReturnBoolean
        [Fact]
        public async void CreateTaskAsync_Executes_ReturnBoolean()
        {
            var task = _tasks.First();

            var _workItemContext = MoqGeneric.Mock.Create<WorkItemContext>();

            MoqGeneric.Mock.Arrange(() => _workItemContext.Set<Task.Models.Task>().FindAsync(task)).DoNothing();

            var _entityRepository = MoqGeneric.Mock.Create<EntityRepository<Task.Models.Task>>(_workItemContext);
            
            var actual = await _entityRepository.CreateAsync(task);

            Assert.IsType<bool>(actual);

            Assert.True(actual);
        }
        #endregion

        #region DeleteAsync_Executes_ReturnBoolean
        [Fact]
        public async void DeleteAsync_Executes_ReturnBoolean()
        {
            var task = _tasks.First();

            var _workItemContext = MoqGeneric.Mock.Create<WorkItemContext>();

            MoqGeneric.Mock.Arrange(() => _workItemContext.Set<Task.Models.Task>().Remove(task)).DoNothing();

            var _entityRepository = MoqGeneric.Mock.Create<EntityRepository<Task.Models.Task>>(_workItemContext);

            var actual = await _entityRepository.DeleteAsync(task);

            Assert.IsType<bool>(actual);

            Assert.True(actual);
        }
        #endregion

        #region GetByIDAsync_Executes_ReturnBoolean
        [Fact]
        public async void GetByIDAsync_Executes_ReturnBoolean()
        {
            var task = _tasks.First();

            var _workItemContext = MoqGeneric.Mock.Create<WorkItemContext>();

            MoqGeneric.Mock.Arrange(() => _workItemContext.Set<Task.Models.Task>().FindAsync(task)).DoNothing();

            var _entityRepository = MoqGeneric.Mock.Create<EntityRepository<Task.Models.Task>>(_workItemContext);

            var actual = await _entityRepository.GetByIDAsync(task.Id);

            Assert.IsAssignableFrom<Task.Models.Task>(actual);
        }
        #endregion

        #region UpdateAsync_Executes_ReturnBoolean
        [Fact]
        public async void UpdateAsync_Executes_ReturnBoolean()
        {
            var task = _tasks.First();

            var _workItemContext = MoqGeneric.Mock.Create<WorkItemContext>();

            MoqGeneric.Mock.Arrange(() => _workItemContext.Set<Task.Models.Task>().Update(task)).DoNothing();

            var _entityRepository = MoqGeneric.Mock.Create<EntityRepository<Task.Models.Task>>(_workItemContext);

            var actual = await _entityRepository.UpdateAsync(task);

            Assert.IsType<bool>(actual);

            Assert.True(actual);
        }
        #endregion
    }
}
