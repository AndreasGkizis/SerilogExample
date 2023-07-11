using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.DependencyResolver;
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

            services.AddTransient<IActionLogRepository, ActionLogRepository>();
            //services.AddTransient<ITeamRepository, TeamRepository>();
        }
    }
}
