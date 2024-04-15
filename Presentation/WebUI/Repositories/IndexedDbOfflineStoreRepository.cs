using Company.CA.Application.Repositories;
using Company.CA.Domain.Models;
using Microsoft.JSInterop;

namespace Company.CA.WebUI.Repositories
{
    public class IndexedDbOfflineStoreRepository : IOfflineStoreRepository
    {
        private readonly IJSRuntime _jsRuntime;
        public IndexedDbOfflineStoreRepository(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<List<Product>> GetProducts()
        {
            try
            {
                return await _jsRuntime.InvokeAsync<List<Product>>("indexedDBFunctions.getData", "products");
            }
            catch (Exception ex)
            {
                // Handle any errors
                Console.WriteLine($"Error retrieving products from IndexedDB: {ex.Message}");
            }

            return new List<Product>();
        }

        public async Task StoreProducts(List<Product> products)
        {
            await _jsRuntime.InvokeVoidAsync("indexedDBFunctions.storeData", "products", products);
        }
    }
}
