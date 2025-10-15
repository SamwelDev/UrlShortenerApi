using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener_Application.Application_Services;
using UrlShortener_Application.Application_Services.Generic_Service;
using UrlShortener_Application.Application_Services.Shortener_Service;
using UrlShortener_Domain.Domain_Models;
using UrlShortener_Infrastructure.Infrastructure_Contexts;
using UrlShortener_Infrastructure.Infrastructure_Repositories.Generic_Repos;

namespace UrlShortener_Infrastructure.Infrastructure_Repositories.Non_Gereic_Repos
{
    public class ShortenerRepos : GenericRepository<ShortUrlModel>, IShortenerSerice
    {
        private readonly ILogger<ShortenerRepos> _logger;
        private readonly ShortenerDbContext _Db;
        public ShortenerRepos(ILogger<ShortenerRepos>logger,ShortenerDbContext dbContext ) : base(dbContext,logger)
        {

            _logger = logger;
            _Db = dbContext;
        }
    }
}
