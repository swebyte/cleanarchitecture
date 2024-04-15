using Company.CA.Application.Repositories;
using Company.CA.Application.Services;
using Company.CA.Domain.Models;
using Moq;

namespace Company.CA.Application.UnitTests
{
    public class ProductServiceTests
    {
        [Fact]
        public async void GetAll_ProductsIsAvailable_ShouldCallAndReturnAllProducts()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();

            productRepositoryMock.Setup(f => f.IsAvailable()).ReturnsAsync(true);
            var productList = new List<Product>()
            {
                new Product() { Id = 1 }
            };
            productRepositoryMock.Setup(f => f.GetAll()).ReturnsAsync(productList);

            var service = CreateService(null, productRepositoryMock);

            // Act
            var actual = await service.GetAll();


            // Assert
            Assert.True(actual.SequenceEqual(productList));
            productRepositoryMock.Verify(f => f.IsAvailable(), Times.Once);
            productRepositoryMock.Verify(f => f.GetAll(), Times.Once);
        }

        [Fact]
        public async void GetAll_ProductsIsAvailable_ShouldStoreProductsInOfflineDb()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(f => f.IsAvailable()).ReturnsAsync(true);

            var offlineStoreRepositoryMock = new Mock<IOfflineStoreRepository>();

            var service = CreateService(offlineStoreRepositoryMock, productRepositoryMock);


            // Act
            var actual = await service.GetAll();

            // Assert
            offlineStoreRepositoryMock.Verify(f => f.StoreProducts(It.IsAny<List<Product>>()), Times.Once);
        }

        [Fact]
        public async void GetAll_ProductsIsNotAvailable_ShouldNotStoreProductsInOfflineDb()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(f => f.IsAvailable()).ReturnsAsync(false);

            var offlineStoreRepositoryMock = new Mock<IOfflineStoreRepository>();

            var service = CreateService(offlineStoreRepositoryMock, productRepositoryMock);

            // Act
            var actual = await service.GetAll();

            // Assert
            offlineStoreRepositoryMock.Verify(f => f.StoreProducts(It.IsAny<List<Product>>()), Times.Never);
        }

        [Fact]
        public async void GetAll_ProductsIsNotAvailable_ShouldReturnOfflineProducts()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(f => f.IsAvailable()).ReturnsAsync(false);

            var offlineStoreRepositoryMock = new Mock<IOfflineStoreRepository>();

            var offlineProductList = new List<Product>()
            {
                new Product() { Id = 1 }
            };

            offlineStoreRepositoryMock.Setup(f => f.GetProducts()).ReturnsAsync(offlineProductList);

            var service = CreateService(offlineStoreRepositoryMock, productRepositoryMock);


            // Act
            var actual = await service.GetAll();

            // Assert
            Assert.True(actual.SequenceEqual(offlineProductList));
            offlineStoreRepositoryMock.Verify(f => f.GetProducts(), Times.Once);
        }


        public static ProductService CreateService(
            Mock<IOfflineStoreRepository>? offlineStoreRepository = null,
            Mock<IProductRepository>? productRepository = null)
        {
            return new ProductService(offlineStoreRepository?.Object ?? new Mock<IOfflineStoreRepository>().Object,
                productRepository?.Object ?? new Mock<IProductRepository>().Object);
        }
    }
}