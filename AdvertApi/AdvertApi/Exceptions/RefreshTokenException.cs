using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertApi.Exceptions
{
    public class RefreshTokenException : Exception
    {
        public RefreshTokenException(string message)
            : base(message)
        {
        }

        public RefreshTokenException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
