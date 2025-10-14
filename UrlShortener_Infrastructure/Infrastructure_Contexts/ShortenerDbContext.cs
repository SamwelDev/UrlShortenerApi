using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener_Domain.Domain_Models;

namespace UrlShortener_Infrastructure.Infrastructure_Contexts
{
    public  class ShortenerDbContext : DbContext
    {
        public ShortenerDbContext(DbContextOptions<ShortenerDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder _builder)
        {
            base.OnModelCreating(_builder);
        }
        public DbSet<ShortUrlModel> ShortUrls { get; set; }
    }
}
