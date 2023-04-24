using Moq;
using Shop.Interfaces;
using Shop.Models;

namespace ShopTests
{
    public class ShopControllerTests
    {
        private Mock<IProductLogic> _productLogic;
        private IList<ProductDto> _productDtos = new List<ProductDto>
        {
            new ProductDto
            {
                Id = 1,
                Name = "test1",
                Description = "test1Desc",
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
                Price = (decimal)99.99
            },
            new ProductDto
            {
                Id = 2,
                Name = "test2",
                Description = "test2Desc",
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
                Price = (decimal)9.99
            },
            new ProductDto
            {
                Id = 3,
                Name = "test3",
                Description = "test3Desc",
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
                Price = (decimal)0.99
            },
        };
        
        public ShopControllerTests()
        {
            _productLogic = new Mock<IProductLogic>();
            _productLogic.Setup(x => x.GetAllProducts()).Returns(Task.FromResult(_productDtos));
            _productLogic.Setup(x => x.GetProductById(It.Is<int>(x => _productDtos.Select(p => p.Id).Contains(x))))
                .Returns(Task.FromResult(_productDtos.First()));
            _productLogic.Setup(x => x.CreateProduct(It.IsAny<ProductDto>())).Returns(Task.CompletedTask);
            _productLogic.Setup(x => x.DeleteProduct(It.Is<int>(x => _productDtos.Select(p => p.Id).Contains(x)))).Returns(Task.CompletedTask);
            _productLogic.Setup(x => x.UpdateProduct(It.IsAny<ProductDto>(), It.IsAny<ProductToUpdateDto>())).Returns(Task.CompletedTask);

        }

        [Fact]
        public async Task GetAllProducts_ReturnsAllProducts()
        {
            var products = await _productLogic.Object.GetAllProducts();
            Assert.NotNull(products);
            Assert.Equal(_productDtos, products);
        }
        [Fact]
        public async Task GetProductById_ReturnsProduct() 
        {
            var product = await _productLogic.Object.GetProductById(1);
            Assert.NotNull(product);            
        }
        [Fact]
        public async Task GetProductById_ReturnsNull()
        {           
            var product = await _productLogic.Object.GetProductById(4);            
            Assert.Null(product);
        }
        [Fact]
        public async Task CreateProduct_RunCreateMethod()
        {
            var product = new ProductDto()
            {
                Id = 4,
                Name = "product4",
                Description = "product4desc",
                Price = (decimal)99.99
            };    
          
            await _productLogic.Object.CreateProduct(product);
            _productLogic.Verify(x => x.CreateProduct(product), Times.Once());
            
        }
        [Fact]
        public async Task DeleteProduct_RunDeleteMethod()
        {
            await _productLogic.Object.DeleteProduct(1);
            _productLogic.Verify(x => x.DeleteProduct(1), Times.Once());
        }
        [Fact]
        public async Task UpdateProduct_RunUpdateMethod()
        {
            var productToUpdate = new ProductToUpdateDto()
            {
                Name = "product3",
                Description = "product3desc",
                Price = (decimal)99.99
            };
            
            await _productLogic.Object.UpdateProduct(_productDtos[1], productToUpdate);
            _productLogic.Verify(x => x.UpdateProduct(_productDtos [1], productToUpdate), Times.Once());
        }

    }


}