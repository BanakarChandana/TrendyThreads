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
            try
            {
                if (product == null)
                    return BadRequest(new { message = "Invalid product data" });

                var result = bl.InsertProduct(product);

                if (result.Contains("Successfully"))
                    return Ok(new { message = result });

                return BadRequest(new { message = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // GET ALL PRODUCTS
        [HttpGet("GetAllProducts")]
        public IActionResult GetAllProducts()
        {
            try
            {
                var products = bl.GetAllProducts();

                if (products == null || products.Count == 0)
                    return NotFound(new { message = "No products found" });

                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // GET PRODUCT BY ID
        [HttpGet("GetProductById/{id}")]
        public IActionResult GetProductById(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(new { message = "Invalid Product Id" });

                var product = bl.GetProductById(id);

                if (product == null)
                    return NotFound(new { message = "Product not found" });

                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // GET PRODUCTS BY DESIGNER
        [HttpGet("GetProductsByDesignerId/{designerId}")]
        public IActionResult GetProductsByDesignerId(int designerId)
        {
            try
            {
                if (designerId <= 0)
                    return BadRequest(new { message = "Invalid Designer Id" });

                var products = bl.GetProductsByDesignerId(designerId);

                if (products == null || products.Count == 0)
                    return NotFound(new { message = "No products found for this designer" });

                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // GET RECENT PRODUCTS
        [HttpGet("GetRecentProducts")]
        public IActionResult GetRecentProducts()
        {
            try
            {
                var products = bl.GetRecentProducts();

                if (products == null || products.Count == 0)
                    return NotFound(new { message = "No recent products found" });

                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // DELETE PRODUCT
        [HttpDelete("DeleteProduct/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(new { message = "Invalid Product Id" });

                var result = bl.DeleteProduct(id);

                if (result.Contains("Successfully"))
                    return Ok(new { message = result });

                return NotFound(new { message = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // ADD PRODUCT TO CART
        [HttpPut("UpdateCart")]
        public IActionResult UpdateCart(int productId, int cartId)
        {
            try
            {
                if (productId <= 0)
                    return BadRequest(new { message = "Invalid ProductId or CartId" });

                var result = bl.UpdateCart(productId, cartId);

                if (result.Contains("Successfully"))
                    return Ok(new { message = result });

                return BadRequest(new { message = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // GET CART PRODUCTS
        [HttpGet("GetCartProducts/{userId}")]
        public IActionResult GetCartProducts(int userId)
        {
            try
            {
                if (userId <= 0)
                    return BadRequest(new { message = "Invalid Cart Id" });

                var products = bl.GetCartProducts(userId);

                if (products == null || products.Count == 0)
                    return NotFound(new { message = "Cart is empty" });

                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}