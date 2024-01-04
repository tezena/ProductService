using Microsoft.AspNetCore.Mvc;
using ProductService.Models;
using ProductService.Models.ViewModels;
using ProductService.Services;

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
        [Route("products:{userId}")]
        public async Task<List<Product>> GetProducts([FromRoute] string userId ) =>
            await _productService.GetProductsAsync(userId);



        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Product>> Get(string id)
        {
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
             var createdProduct = await _productService.CreateAsync(newProduct);

            return  CreatedAtAction("Get", new { id = createdProduct.Id }, createdProduct);
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, ProductVM updateProduct)
        {
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
            var product = await _productService.GetProductByIdAsync(id);

            if (product is null)
            {
                return NotFound();
            }

            await _productService.RemoveAsync(id);

            return NoContent();
        }

    }
}
