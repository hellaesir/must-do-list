using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MustDoList.Dto.Commons
{
    public class ErrorResponse
    {
        private Exception exception;

        public ErrorResponse()
        {
        }

        public ErrorResponse(Exception exception)
        {
            this.exception = exception;
            this.Detailed = exception.Message;
            this.Message = exception.ToString();
        }

        public string Detailed { get; set; }
        public string Message { get; set; }
    }
}
