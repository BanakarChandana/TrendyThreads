using Microsoft.AspNetCore.Mvc;
using ThrendyThreads.BusinessLayer;
using ThrendyThreads.Models;

namespace ThrendyThreads.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        BLProduct bl = new BLProduct();

        // ADD PRODUCT
        [HttpPost("AddProduct")]
        public IActionResult AddProduct([FromBody] ProductModel product)
        {
            if (product == null)
                return BadRequest("Invalid product data");

            var result = bl.InsertProduct(product);

            if (result.Contains("Successfully"))
                return Ok(new { message = result });

            return BadRequest(new { message = result });
        }

        // GET ALL PRODUCTS
        [HttpGet("GetAllProducts")]
        public IActionResult GetAllProducts()
        {
            var products = bl.GetAllProducts();

            if (products == null || products.Count == 0)
                return NotFound(new { message = "No products found" });

            return Ok(products);
        }

        // GET PRODUCT BY ID
        [HttpGet("GetProductById/{id}")]
        public IActionResult GetProductById(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Product Id");

            var product = bl.GetProductById(id);

            if (product == null)
                return NotFound(new { message = "Product not found" });

            return Ok(product);
        }

        // GET PRODUCTS BY DESIGNER ID
        [HttpGet("GetProductsByDesignerId/{designerId}")]
        public IActionResult GetProductsByDesignerId(int designerId)
        {
            if (designerId <= 0)
                return BadRequest("Invalid Designer Id");

            var products = bl.GetProductsByDesignerId(designerId);

            if (products == null || products.Count == 0)
                return NotFound(new { message = "No products found for this designer" });

            return Ok(products);
        }

        // GET RECENT PRODUCTS (TOP 4)
        [HttpGet("GetRecentProducts")]
        public IActionResult GetRecentProducts()
        {
            var products = bl.GetRecentProducts();

            if (products == null || products.Count == 0)
                return NotFound(new { message = "No recent products found" });

            return Ok(products);
        }

        // DELETE PRODUCT
        [HttpDelete("DeleteProduct/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Product Id");

            var result = bl.DeleteProduct(id);

            if (result.Contains("Successfully"))
                return Ok(new { message = result });

            return NotFound(new { message = result });
        }
    }
}