using Moq;
using Shop.Controllers;
using Shop.Interfaces;
using Shop.Models;
using Xunit;

namespace ShopUnitTests
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
        }

        [Fact]
        public void GetAllProductsTest()
        {
            //Arrange
            var products = _productLogic.Object.GetAllProducts();
            //Act
            Assert.NotNull(products);
            Assert.Equal(_productDtos, products.Result);
        }
    }


}