using Microsoft.AspNetCore.Mvc;
using TestRedis.Agents;
using TestRedis.ClientResponse;
using TestRedis.EFModel;
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
        [Route("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            Response Response = new Response();
            try
            {
                Response.Data = _userAgent.GetAllUsers();
                Response.Status = true;
                Response.Message = "Data get successfully.";
            }
            catch (Exception)
            {
                Response.Status = false;
                Response.Message = "failure.";
            }
            return Ok(Response);
        }

        [HttpGet]
        [Route("GetUserByEmailId")]
        public IActionResult GetUserByEmailId(string email)
        {
            Response Response = new Response();
            try
            {
                Response.Data = _userAgent.GetUserByEmailId(email);
                Response.Status = true;
                Response.Message = "Data get successfully.";
            }
            catch (Exception)
            {
                Response.Status = false;
                Response.Message = "failure.";
            }
            return Ok(Response);
        }

        [HttpPost]
        [Route("AddUser")]
        public IActionResult AddUser(UserResponse user)
        {
            Response Response = new Response();
            try
            {
                Response = _userAgent.AddUser(user);
                Response.Status = true;
            }
            catch (Exception)
            {
                Response.Status = false;
                Response.Message = "failure.";
            }
            return Ok(Response);
        }

        [HttpPost]
        [Route("UpdateUser")]
        public IActionResult UpdateUser(UserResponse user)
        {
            Response Response = new Response();
            try
            {
                Response = _userAgent.UpdateUser(user);
                Response.Status = true;
            }
            catch (Exception)
            {
                Response.Status = false;
                Response.Message = "failure.";
            }
            return Ok(Response);
        }


    }
}
