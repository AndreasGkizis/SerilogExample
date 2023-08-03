using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Logger;
using Database.DependencyResolver;
using Microsoft.AspNetCore.Builder;
using Serilog;
using RepositoryInterfaces.Interfaces;
using Infrastructure.Repositories;

namespace Infrastructure.DependencyResolver
{
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastructure(
            this IServiceCollection services,
             IConfiguration configuration,
            string connectionStringName)
        {
            services.AddDatabaseContext(configuration, connectionStringName);
            services.AddTransient<IUserRepository, UserRepository>();
        }

        public static void AddLoggerConfig(this WebApplicationBuilder builder)
        {
            builder.AddLoggerConfiguration();
            // adds Serilog to the request pipeline 
            builder.Host.UseSerilog();
        }
    }
}
