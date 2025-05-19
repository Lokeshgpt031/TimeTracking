using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TimeTracking.Controllers
{
    [Route("")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        // GET: api/<HomeController>
        [HttpGet]
        [ProducesResponseType<IEnumerable<Resources.Resource>>(StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            var resources = new List<Resources.Resource>
            {
                new Resources.Resource("Employees", "/api/Employee"),
                new Resources.Resource("Projects", "/api/Project"),
                new Resources.Resource("TimeEntries", "/api/TimeEntry"),
                new Resources.Resource("ProjectAssignments", "/api/ProjectAssignment")
            };
            return Ok(resources);
        }
    }
}
