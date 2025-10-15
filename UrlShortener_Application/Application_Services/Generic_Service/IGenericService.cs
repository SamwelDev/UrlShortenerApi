using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UrlShortener_Application.Application_DTOs.DTOs_Shortener;

namespace UrlShortener_Application.Application_Services.Generic_Service
{
    public interface IGenericService<TModel> where TModel : class 
    {
        //linked to generic repository
        Task<ResponseDto<List<TModel>>> FindAsync(Expression<Func<TModel, bool>> predicate);
        Task<ResponseDto<TModel>> GetByIdAsync(int id);
        Task<ResponseDto<List<TModel>>> GetAllAsync();
        Task<ResponseDto<TModel>> AddAsync(TModel model);
        Task<ResponseDto<object>> DeleteAsync(int id);
        Task<ResponseDto<TModel>> UpdateAsync(TModel model);
    }
   
}
