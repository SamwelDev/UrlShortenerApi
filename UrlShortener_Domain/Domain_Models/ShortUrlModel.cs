using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener_Domain.Domain_Models
{
    public  class ShortUrlModel
    {
        public int Id { get; set; }
        public int ClickCount { get; set; } = 0;
        public string OriginalUrl { get; set; } = string.Empty;
        public string ShortCode { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
