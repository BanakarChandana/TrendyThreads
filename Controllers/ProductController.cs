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

        // POST PRODUCT
        [HttpPost("AddProduct")]
        public IActionResult AddProduct([FromBody] ProductModel product)
        {
            var result = bl.InsertProduct(product);

            if (result.Contains("Successfully"))
                return Ok(result);
            else
                return BadRequest(result);
        }

        // GET ALL PRODUCTS
        [HttpGet("GetAllProducts")]
        public IActionResult GetAllProducts()
        {
            var products = bl.GetAllProducts();

            if (products == null || products.Count == 0)
                return NotFound("No products found");

            return Ok(products);
        }

        // GET PRODUCT BY ID
        [HttpGet("GetProductById/{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = bl.GetProductById(id);

            if (product == null)
                return NotFound("Product not found");

            return Ok(product);
        }

        // GET RECENT 4 PRODUCTS
        [HttpGet("GetRecentProducts")]
        public IActionResult GetRecentProducts()
        {
            var products = bl.GetRecentProducts();

            if (products == null || products.Count == 0)
                return NotFound("No recent products found");

            return Ok(products);
        }

        // DELETE PRODUCT
        [HttpDelete("DeleteProduct/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var result = bl.DeleteProduct(id);

            if (result.Contains("Successfully"))
                return Ok(result);
            else
                return NotFound(result);
        }
    }
}