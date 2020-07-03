using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertApi.Exceptions
{
    public class ClientDoesntExistsException :Exception
    {
        public ClientDoesntExistsException(string message)
            : base(message)
        {
        }

        public ClientDoesntExistsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
