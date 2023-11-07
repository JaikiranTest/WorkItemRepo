using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WorkItem.Task.Services.Contracts;

namespace WorkItem.Task.Controllers
{
    public class TasksController : Controller
    {
        private readonly ITasksService _tasksService;

        public TasksController(ITasksService tasksService)
        {
            _tasksService = tasksService;
        }

        // GET: Tasks
        public async Task<IActionResult> Index()
        {
            var result = await _tasksService.GetTasksAsync();
            return View(result);
        }

        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _tasksService.GetTaskByIDAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // GET: Tasks/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["StatusId"] = new SelectList(new Models.TaskStatus[] { new Models.TaskStatus { Id = 1, Status = "To Do" } }, "Id", "Status");
            
            var userProfiles = await _tasksService.GetUserProfilesAsync();

            ViewData["Emails"] = new SelectList(userProfiles.Select(x => x?.EmailId).ToList());

            return View();
        }

        // POST: Tasks/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Title,Assigned,StatusId,Discription,Comments")] Models.Task task)
        {
            if (ModelState.IsValid)
            {
                await _tasksService.CreateTaskAsync(task);
                return RedirectToAction(nameof(Index));
            }

            ViewData["StatusId"] = new SelectList(await _tasksService.GetTaskStatusAsync(), "Id", "Status", task.StatusId);
            ViewData["EmailId"] = new SelectList(await _tasksService.GetUserProfilesAsync(), "Id", "EmailID", task.Assigned);

            return View(task);
        }

        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _tasksService.GetTaskByIDAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            ViewData["StatusId"] = new SelectList(await _tasksService.GetTaskStatusAsync(), "Id", "Status", task.StatusId);

            var userProfiles = await _tasksService.GetUserProfilesAsync();

            ViewData["Emails"] = new SelectList(userProfiles.Select(x => x?.EmailId).ToList());

            return View(task);
        }

        // POST: Tasks/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Assigned,StatusId,Discription,Comments")] Models.Task task)
        {
            if (id != task.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (!await _tasksService.UpdateTaskAsync(task))
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["StatusId"] = new SelectList(await _tasksService.GetTaskStatusAsync(), "Id", "Status", task.StatusId);

            return View(task);
        }

        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _tasksService.GetTaskByIDAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _tasksService.GetTasksAsync() == null)
            {
                return Problem("No Work Items to delete!.");
            }
            var task = await _tasksService.GetTaskByIDAsync(id);

            if (task != null)
            {
                if (await _tasksService.DeleteTaskAsync(task))
                    return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }
    }
}
