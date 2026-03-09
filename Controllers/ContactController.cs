using Microsoft.AspNetCore.Mvc;
using ThrendyThreads.BusinessLayer;
using ThrendyThreads.Models;

namespace ThrendyThreads.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        BLContact bl = new BLContact();

        [HttpPost("AddContact")]
        public IActionResult AddContact([FromBody] ContactModel contact)
        {
            var result = bl.InsertContact(contact);

            if (result.Contains("Successfully"))
                return Ok(result);
            else
                return BadRequest(result);
        }
    }
}