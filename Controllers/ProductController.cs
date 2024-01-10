using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductService.Models;
using ProductService.Models.ViewModels;
using ProductService.Services;
using System.Security.Claims;

namespace ProductService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController:ControllerBase
    {

        private readonly ProductServices _productService;

        public ProductController(ProductServices productService) =>
            _productService = productService;

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var userId = GetCurrentUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            return await _productService.GetProductsAsync(userId);

        }



        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Product>> Get(string id)
        {
            var userId = GetCurrentUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            var product = await _productService.GetProductByIdAsync(id);

            if (product is null)
            {
                return NotFound();
            }

            return product;
        }


        [HttpPost]
        public async Task<IActionResult> Post(ProductVM newProduct)
        {

            var userId = GetCurrentUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            var createdProduct = await _productService.CreateAsync(newProduct,userId);

            return  CreatedAtAction("Get", new { id = createdProduct.Id }, createdProduct);
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, ProductVM updateProduct)
        {

            var userId = GetCurrentUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            var product = await _productService.GetProductByIdAsync(id);

            if (product is null)
            {
                return NotFound();
            }

    

            await _productService.UpdateAsync(id, updateProduct);

            return NoContent();
        }

        [HttpDelete]

        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var userId = GetCurrentUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            var product = await _productService.GetProductByIdAsync(id);

            if (product is null)
            {
                return NotFound();
            }

            await _productService.RemoveAsync(id);

            return NoContent();
        }



        private string GetCurrentUserId()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;
                return userClaims.FirstOrDefault(x => x.Type == ClaimTypes.DenyOnlyPrimarySid)?.Value;
              
            }
            return null;

        }

    }
}
