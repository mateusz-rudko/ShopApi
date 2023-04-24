using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Interfaces;
using Shop.Models;

namespace Shop.Logic
{
    public class ProductLogic : IProductLogic
    {
        private readonly ProductsDbContext _database;
        public ProductLogic(ProductsDbContext database) 
        {
            _database = database;
        }
        public async Task CreateProduct(ProductDto product)
        {
            await _database.Products.AddAsync(product);
            await _database.SaveChangesAsync();
        }

        public async Task DeleteProduct(int productId)
        {
            var product = await _database.Products.FirstOrDefaultAsync(p => p.Id == productId);
            _database.Remove(product);
            await _database.SaveChangesAsync();
        }

        public async Task<IList<ProductDto>> GetAllProducts()
        {
            var products = await _database.Products.ToListAsync();
            return products;
        }

        public async Task<ProductDto> GetProductById(int id)
        {
            var product = await _database.Products.FirstOrDefaultAsync(p => p.Id == id);
            return product;
        }

        public async Task UpdateProduct(ProductDto productDto, ProductToUpdateDto productToUpdateDto)
        {
            productDto.Name = productToUpdateDto.Name;
            productDto.Description = productToUpdateDto.Description;
            productDto.Price = productToUpdateDto.Price;
            productDto.LastModifiedDate = DateTime.Now;
            await _database.SaveChangesAsync();
        }
    }
}
