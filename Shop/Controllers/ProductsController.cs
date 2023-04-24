using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Interfaces;
using Shop.Models;

namespace Shop.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductLogic _productLogic;
        public ProductsController(IProductLogic productLogic)
        {
            _productLogic = productLogic;
        }
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
        {
            var products = await _productLogic.GetAllProducts();
            if(products.Count == 0)
            {
                return NotFound("Empty list of products.");
            }
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _productLogic.GetProductById(id);
            if(product == null)
            {
                return NotFound("Product does not exist.");
            }
            return Ok(product);
        }
        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProductAsync(ProductDto product)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Product is not valid!");
            }
            await _productLogic.CreateProduct(product);
            return Ok(product);
        }
        [HttpPut("{productId}")]
        public async Task<ActionResult<ProductDto>> UpdateProduct(int productId, ProductToUpdateDto productToUpdate)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Product is not valid!");
            }
            var product = await _productLogic.GetProductById(productId);
            if(product == null)
            {
                return NotFound("Product do not exist.");
            }
            await _productLogic.UpdateProduct(product, productToUpdate);
            return Ok(product);
        }

        [HttpDelete("{productId}")]
        public async Task<ActionResult> DeleteProduct(int productId)
        {
            var product = await _productLogic.GetProductById(productId);
            if(product == null)
            {
                return NotFound("Product do not exist.");
            }
            await _productLogic.DeleteProduct(product.Id);
            return NoContent();
        }

    }
}
