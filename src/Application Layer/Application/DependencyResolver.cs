using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyResolver
    {
        public static IServiceCollection AddApplication(
          this IServiceCollection services
            )
        {

            //services.AddScoped<Ikati, kati>();

            return services;
        }
    }
}