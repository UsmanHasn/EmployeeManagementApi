using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BusinesObjectLayer.Dtos
{
    public class BOL_ApiResponse<T>
    {
        public T? Data { get; set; }

        //Enum HttpStatusCode
        public HttpStatusCode StatusCode { get; set; }
        public string? Message { get; set; }

        //public ApiResponse(T data, int statusCode, string message)
        //{
        //    Data = data;
        //    StatusCode = statusCode;
        //    Message = message;
        //}
    }
}
