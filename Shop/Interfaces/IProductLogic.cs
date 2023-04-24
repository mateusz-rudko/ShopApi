using Shop.Models;

namespace Shop.Interfaces
{
    public interface IProductLogic
    {
        Task<ProductDto> GetProductById(int id);
        Task<IList<ProductDto>> GetAllProducts();
        Task CreateProduct(ProductDto product);
        Task UpdateProduct(ProductDto productDto, ProductToUpdateDto productToUpdateDto);
        Task DeleteProduct(int id);

    }

}
