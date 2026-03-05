using Microsoft.AspNetCore.Mvc;
using ThrendyThreads.BusinessLayer;
using ThrendyThreads.Model;

namespace ThrendyThreads.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {

        BLRegistration objBL = new BLRegistration();

        [HttpPost("RegisterUser")]
        public IActionResult RegisterUser([FromBody] RegisterModel model)
        {
            try
            {
                int result = objBL.InsertRegistration(model);

                if (result > 0)
                {
                    return Ok("User Registered Successfully");
                }
                else
                {
                    return BadRequest("Registration Failed");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}