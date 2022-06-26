using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aranda.CatalogProductCore.Repository.Dto
{
    public class ApiResponse<T>
    {
        public ApiResponse(T data)
        {
            Data = data;
            IsSuccess = true;
            ReturnMessage = "";
        }
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public string ReturnMessage { get; set; }
    }
}