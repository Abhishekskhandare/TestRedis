using Microsoft.AspNetCore.Mvc;
using TestRedis.Agents;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TestRedis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserAgent _userAgent;
        public UserController(IUserAgent userAgent)
        {
            _userAgent = userAgent;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var httpResponse = new
            {
                Status = true, // or false for an error
                Message = "Data get successfully.",
                Data = _userAgent.GetAllUsers()
            };
            return Ok(httpResponse);
        }
    }
}
