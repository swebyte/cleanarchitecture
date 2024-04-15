using Company.CA.Domain.Models;

namespace Company.CA.Application.Repositories
{
    public interface IProductRepository
    {
        public Task<bool> IsAvailable();
        public Task<List<Product>> GetAll();
    }
}
