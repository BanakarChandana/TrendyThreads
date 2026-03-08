using Microsoft.AspNetCore.Mvc;
using ThrendyThreads.BusinessLayer;
using ThrendyThreads.Model;
using System.Collections.Generic;
using System.Linq;

namespace ThrendyThreads.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly BLRegistration _objBL = new BLRegistration();

        // POST: api/Registration/RegisterUser
        [HttpPost("RegisterUser")]
        public IActionResult RegisterUser([FromBody] RegisterModel model)
        {
            if (model == null)
                return BadRequest(new { message = "Invalid user data" });

            try
            {
                int result = _objBL.InsertRegistration(model);

                if (result > 0)
                    return Ok(new { message = "User Registered Successfully" });

                return BadRequest(new { message = "Registration Failed" });
            }
            catch (Exception ex)
            {
                // Optionally log ex.Message here
                return BadRequest(new { error = ex.Message });
            }
        }

        // GET: api/Registration/GetAllUsers
        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            try
            {
                List<RegisterModel> users = _objBL.GetAllUsers();

                if (users == null || users.Count == 0)
                    return NotFound(new { message = "No users found" });

                // Only return safe fields
                var result = users.Select(u => new
                {
                    u.UserName,
                    u.Email,
                    u.Image
                }).ToList();

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Optionally log ex.Message here
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}