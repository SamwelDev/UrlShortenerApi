using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener_Application.Application_Services.Generic_Service;
using UrlShortener_Domain.Domain_Models;

namespace UrlShortener_Application.Application_Services.Shortener_Service
{
    public interface IShortenerSerice : IGenericService<ShortUrlModel>
    {
    }
}
