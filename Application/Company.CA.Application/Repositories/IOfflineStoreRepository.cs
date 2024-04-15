using Company.CA.Domain.Models;

namespace Company.CA.Application.Repositories
{
    public interface IOfflineStoreRepository
    {
        public Task StoreProducts(List<Product> products);
        public Task<List<Product>> GetProducts();
    }
}
