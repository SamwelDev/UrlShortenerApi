using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener_Application.Application_DTOs.DTOs_Shortener;

namespace UrlShortener_Application.Application_DTOs.DTOs_Othres
{
    public  class ResponseHelper
    {
        public static ResponseDto<T> SetSuccess<T>(T? data = default, string? message = "Success!", string? description = "Success!", string? token = null, int CurrentPage = 1, int TotalCount = 1, int PageSize = 500)
        {
            return CreateResponse(SignalStatus.Success, data, message, description, token, CurrentPage, TotalCount, PageSize);
        }

        public static ResponseDto<T> SetBadResponse<T>(T? data = default, string? message = "Bad Request", string? description = "Bad Request")
        {
            return CreateResponse(SignalStatus.BadRequest, data, message, description);
        }

        public static ResponseDto<T> SetBadRequest<T>(T? data = default, string message = "Invalid request")
        {
            return CreateResponse(SignalStatus.BadRequest, data, message);
        }

        public static ResponseDto<T> SetNoContent<T>(T? data = default, string message = "No Content")
        {
            return CreateResponse(SignalStatus.NoContent, data, message);
        }



        public static ResponseDto<T> SetNotFound<T>(T? data = default, string message = "Not found")
        {
            return CreateResponse(SignalStatus.NotFound, data, message);
        }

        public static ResponseDto<T> SetInternalServerError<T>(T? data = default, string message = "Unexpected error, contact admin")
        {
            return CreateResponse(SignalStatus.InternalServerError, data, message);
        }

        private static ResponseDto<T> CreateResponse<T>(SignalStatus status, T? data = default, string? message = null, string? description = null, string? token = null, int CurrentPage = 1, int TotalCount = 1, int PageSize = 500)
        {
            return new ResponseDto<T>
            {
                Status = (int)status,
                Message = message,
                Description = description,
                Data = data,
                Token = token,
                CurrentPage = CurrentPage,
                PageSize = PageSize,
                TotalCount = TotalCount,
            };
        }
    }
}
