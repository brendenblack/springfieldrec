using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringfieldRecMvc.Infrastructure
{
    public class HttpException : Exception
    {
        public int StatusCode { get; private set; }
        public HttpException(int statusCode, String message) : base(message)
        {
            this.StatusCode = statusCode;
        }
    }
}
