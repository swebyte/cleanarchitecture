using Company.CA.Application.Repositories;
using Company.CA.Domain.Models;

namespace Company.CA.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IOfflineStoreRepository _offlineStoreRepository;
        private readonly IProductRepository _productRepository;
        public ProductService(IOfflineStoreRepository offlineStoreRepository, IProductRepository productRepository)
        {
            _offlineStoreRepository = offlineStoreRepository;
            _productRepository = productRepository;
        }

        public async Task<List<Product>> GetAll()
        {
            if (await _productRepository.IsAvailable())
            {
                var products = await _productRepository.GetAll();
                await _offlineStoreRepository.StoreProducts(products);
                return products;
            }

            return await _offlineStoreRepository.GetProducts();
        }
    }
}
