using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DependencyResolver
{
    public static class DependencyResolver
    {
        public static IServiceCollection AddDatabaseContext(
            this IServiceCollection services, 
            IConfiguration configuration, 
            string connectionStringName)
        {

            services.AddDbContext<ExampleDbContext>
            (options => options.
                UseSqlServer(configuration.GetConnectionString(connectionStringName)));
            return services;
        }
    }
}
