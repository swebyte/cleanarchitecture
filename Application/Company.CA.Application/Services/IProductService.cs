using Company.CA.Domain.Models;

namespace Company.CA.Application.Services
{
    public interface IProductService
    {
        public Task<List<Product>> GetAll();
    }
}
