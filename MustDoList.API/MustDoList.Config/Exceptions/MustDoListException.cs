using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MustDoList.Config.Exceptions
{
    public class MustDoListException : Exception
    {
        public MustDoListException()
        {
        }

        public MustDoListException(string? message) : base(message)
        {
        }

        public MustDoListException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
