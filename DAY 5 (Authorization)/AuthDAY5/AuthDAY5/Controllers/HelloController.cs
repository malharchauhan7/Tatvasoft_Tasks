using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthDAY5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelloController : ControllerBase
    {
        [HttpGet("Admin")]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult HelloAdmin()
        {
            return Ok("Hello Admin");
        }

        [HttpGet("User")]
        [Authorize(Policy = "UserOnly")]
        public IActionResult HelloUser()
        {
            return Ok("Hello User");
        }
    }
}