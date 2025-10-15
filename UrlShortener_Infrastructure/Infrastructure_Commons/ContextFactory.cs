using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener_Infrastructure.Infrastructure_Contexts;

namespace UrlShortener_Infrastructure.Infrastructure_Commons
{
    public class ContextFactory : IDesignTimeDbContextFactory<ShortenerDbContext>
    {   public ShortenerDbContext CreateDbContext(string[] args)
        {
            var _optBuilder = new DbContextOptionsBuilder<ShortenerDbContext>();
            _optBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;;Database=ShortnerDbContext-v0.1;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true");
            return new ShortenerDbContext(_optBuilder.Options);
        }
    }
}
