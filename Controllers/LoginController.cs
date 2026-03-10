using Microsoft.AspNetCore.Mvc;
using ThrendyThreads.BusinessLayer;
using ThrendyThreads.Model;

namespace ThrendyThreads.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        BLLogin bl = new BLLogin();

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            bool result = bl.LoginUser(login);

            if (result)
                return Ok("Login Successful");
            else
                return Unauthorized("Invalid Email or Password");
        }
    }
}