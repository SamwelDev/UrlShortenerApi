using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener_Application.Application_Services.Generic_Service;
using UrlShortener_Application.Application_Services.Shortener_Service;
using UrlShortener_Infrastructure.Infrastructure_Contexts;
using UrlShortener_Infrastructure.Infrastructure_Repositories.Generic_Repos;
using UrlShortener_Infrastructure.Infrastructure_Repositories.Non_Gereic_Repos;

namespace UrlShortener_Infrastructure.Infrastructure_Commons
{
    public static class ContextDi
    {
        public static IServiceCollection AddContextDI(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment, ILogger _logger)
        {

            _logger.LogInformation("Starting infrastructure service configuration for: {Environment}", environment.EnvironmentName);

            var connectionName = environment.IsDevelopment() ? "SqlServerDefaultConnection-Dev" : "SqlServerDefaultConnection-Live";
            var connectionString = configuration.GetConnectionString(connectionName);

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                _logger.LogError("No connection string found for: {ConnectionName}", connectionName);
                throw new InvalidOperationException($"Missing connection string: {connectionName}");
            }

            _logger.LogInformation("Using connection string: {ConnectionName}", connectionName);

            services.AddDbContext<ShortenerDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            // -------- later authorization and authetication services ----//
            // -------- ********************************************* ----//

            //--- GENERIC SERVICES ---- //
            //-------**********--------//
            services.AddScoped(typeof(IGenericService<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IShortenerSerice), typeof(ShortenerRepos));
            _logger.LogInformation("All services are registered successfully.");
            return services;
        }

    }
}
