using Company.CA.Application.Repositories;
using Company.CA.Application.Services;
using Company.CA.Infrastructure.Data.Repositories;
using Company.CA.WebUI;
using Company.CA.WebUI.Repositories;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using CA.Infrastructure.Extensions;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOfflineStoreRepository, IndexedDbOfflineStoreRepository>();

builder.Services.AddScoped<IProductService, ProductService>();


builder.Services.AddFakeHtpClient("https://dummyjson.com/");

await builder.Build().RunAsync();
