using Microsoft.AspNetCore.Mvc;
using ThrendyThreads.BusinessLayer;
using ThrendyThreads.Model;
using System.Collections.Generic;

namespace ThrendyThreads.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OutfitChangeRequestController : ControllerBase
    {
        BLOutfitChangeRequest objBL = new BLOutfitChangeRequest();

        // POST: api/OutfitChangeRequest/SendRequest
        [HttpPost("SendRequest")]
        public IActionResult SendRequest([FromBody] OutfitChangeRequestModel model)
        {
            if (model == null)
                return BadRequest(new { message = "Invalid request data" });

            try
            {
                int result = objBL.InsertOutfitChangeRequest(model);

                if (result > 0)
                {
                    return Ok(new
                    {
                        message = "Request Sent Successfully"
                    });
                }

                return BadRequest(new { message = "Insert Failed" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // GET: api/OutfitChangeRequest/GetAllRequests
        [HttpGet("GetAllRequests")]
        public IActionResult GetAllRequests()
        {
            try
            {
                List<OutfitChangeRequestModel> requests = objBL.GetAllRequests();

                if (requests == null || requests.Count == 0)
                    return NotFound(new { message = "No requests found" });

                return Ok(requests);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        [HttpGet("GetRequestsByDesigner/{designerId}")]
        public IActionResult GetRequestsByDesigner(int designerId)
        {
            try
            {
                var requests = objBL.GetRequestsByDesignerId(designerId);

                if (requests == null || requests.Count == 0)
                    return NotFound("No requests found for this designer");

                return Ok(requests);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}