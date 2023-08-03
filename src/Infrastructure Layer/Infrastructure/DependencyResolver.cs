using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.DependencyResolver;
using RepositoryInterfaces.Interfaces;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Serilog;

namespace Infrastructure.DependencyResolver
{
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration,
            string DBconnectionStringName
            )
        {
            services.AddDatabaseContext(configuration, DBconnectionStringName);
            services.AddTransient<IUserRepository, UserRepository>();
        }

        public static void AddLoggerConfig(
            this WebApplicationBuilder builder
            )
        {
            builder.AddLoggerConfiguration();
        }
    }
}
