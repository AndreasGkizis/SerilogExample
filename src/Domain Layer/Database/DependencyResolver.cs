using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Database.DependencyResolver
{
    public static class DependencyResolver
    {
        public static IServiceCollection AddDatabaseContext(
            this IServiceCollection services, 
            IConfiguration configuration, 
            string connectionStringName)
        {

            services.AddDbContext<LoggingDbContext>
            (options => options.
                UseSqlServer(configuration.GetConnectionString(connectionStringName)));
            return services;
        }
    }
}
