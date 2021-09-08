using Api.CustomAttributes;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        [HttpGet]
        [Auth]
        public IActionResult Get() => Ok(Database.Database.GetAllEmployees());
    }
}