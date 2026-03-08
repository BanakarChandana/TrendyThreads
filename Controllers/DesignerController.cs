using Microsoft.AspNetCore.Mvc;
using ThrendyThreads.BusinessLayer;
using ThrendyThreads.Models;

namespace ThrendyThreads.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignerController : ControllerBase
    {
        BLDesigner bl = new BLDesigner();

        // POST
        [HttpPost("AddDesigner")]
        public IActionResult AddDesigner([FromBody] DesignerModel designer)
        {
            var result = bl.InsertDesigner(designer);

            if (result.Contains("Successfully"))
                return Ok(result);
            else
                return BadRequest(result);
        }

        // GET ALL
        [HttpGet("GetAllDesigners")]
        public IActionResult GetAllDesigners()
        {
            var designers = bl.GetAllDesigners();

            if (designers.Count == 0)
                return NotFound("No designers found");

            return Ok(designers);
        }

        // GET BY ID
        [HttpGet("GetDesignerById/{id}")]
        public IActionResult GetDesignerById(int id)
        {
            var designer = bl.GetDesignerById(id);

            if (designer == null)
                return NotFound("Designer not found");

            return Ok(designer);
        }

        // DELETE
        [HttpDelete("DeleteDesigner/{id}")]
        public IActionResult DeleteDesigner(int id)
        {
            var result = bl.DeleteDesigner(id);

            if (result.Contains("Successfully"))
                return Ok(result);
            else
                return NotFound(result);
        }
    }
}