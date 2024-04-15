using Microsoft.Extensions.DependencyInjection;

namespace CA.Infrastructure.Extensions
{
    public static class InitExtensions
    {
        public static IServiceCollection AddFakeHtpClient(this IServiceCollection services, string baseAddress)
        {
            services.AddHttpClient(
              "fakeapi",
              client =>
              {
                  // Set the base address of the named client.
                  client.BaseAddress = new Uri(baseAddress);

                  // Add a user-agent default request header.
                  //client.DefaultRequestHeaders.UserAgent.ParseAdd("dotnet-docs");
              });
            return services;
        }
    }
}