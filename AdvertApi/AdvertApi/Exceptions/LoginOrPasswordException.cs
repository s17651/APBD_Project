using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertApi.Exceptions
{
    public class LoginOrPasswordException : Exception
    {
        public LoginOrPasswordException(string message)
           : base(message)
        {
        }

        public LoginOrPasswordException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
