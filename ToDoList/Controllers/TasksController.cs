using Business;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoList.ActionFilter;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(MyAuthActionFilter))]
    public class TasksController : ControllerBase
    {
        private ITaskService _taskService;
        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAllTasks(int projectId)
        {
            var tasks = await _taskService.GetAllTasks(projectId);
            return Ok(tasks);
        }
    }
}
