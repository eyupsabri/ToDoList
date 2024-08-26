using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using ToDoList.ActionFilter;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(MyAuthActionFilter))]
    public class ProjectsController : ControllerBase
    {
        private IProjectService _projectService;
        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetProjects()
        {
            if (HttpContext.Items["Email"] is string email)
            {
                var projects = await _projectService.GetAllProjectsForUser(email);
                return Ok(projects);
            }
            return BadRequest();

        }
        //[HttpGet]
        //[Route("[action]/{projectId}")]
        //public async Task<IActionResult> GetProject(int projectId)
        //{
        //    if (HttpContext.Items["Email"] is string email)
        //    {
        //        var projects = await _projectService.GetAllProjectsForUser(email);
        //        return Ok(projects);
        //    }
        //    return BadRequest();

        //}
    }
}
