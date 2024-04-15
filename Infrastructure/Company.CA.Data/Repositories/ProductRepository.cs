using Company.CA.Application.Repositories;
using Company.CA.Domain.Models;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Text.Json;

namespace Company.CA.Infrastructure.Data.Repositories
{
    class Temp
    {
        public List<Product> products { get; set; }
    }

    public class ProductRepository : IProductRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ProductRepository> _logger = null!;
        public ProductRepository(IHttpClientFactory httpClientFactory, ILogger<ProductRepository> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<List<Product>> GetAll()
        {
            using HttpClient client = _httpClientFactory.CreateClient("fakeapi");

            try
            {
                Temp? temp = await client.GetFromJsonAsync<Temp>(
                    $"products",
                    new JsonSerializerOptions(JsonSerializerDefaults.Web));

                return temp.products ?? [];
            }
            catch (Exception ex)
            {
                _logger.LogError("Error getting something fun to say: {Error}", ex);
            }

            return [];
        }

        public async Task<bool> IsAvailable()
        {
            using HttpClient client = _httpClientFactory.CreateClient("fakeapi");

            try
            {
                // Send a HEAD request to the specified URL
                HttpResponseMessage response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Head, client.BaseAddress));

                // Return true if the request was successful
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                // If any exception occurs, return false
                return false;
            }
        }

    }
}
