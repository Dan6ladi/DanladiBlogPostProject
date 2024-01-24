using BlogPost.SharedKernel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BlogPost.SharedKernel
{
    public class ResponseModel
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public List<string>? Errors { get; set; }
        public int Count { get; set; }
    }

    public class ResponseModel<T> : ResponseModel
    {
        public T Data { get; set; }
        public IEnumerable<T> Result { get; set; }
        public PaginationResponse Pager { get; set; }

        public static ResponseModel<T> CreateResponse(bool status, string message, T data)
        {
            var response = new ResponseModel<T>
            {
                Status = status,
                Message = message ?? "",
                Data = data
            };

            return response;
        }
    }
}
