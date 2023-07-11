using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyResolver
    {
        public static IServiceCollection AddApplication(
          this IServiceCollection services,
          IConfiguration configuration,
          string connectionStringName)
        {

            //services.AddScoped<Ikati, kati>();

            return services;
        }
    }
}