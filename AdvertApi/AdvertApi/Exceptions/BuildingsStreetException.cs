using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertApi.Exceptions
{
    public class BuildingsStreetException : Exception
    {
        public BuildingsStreetException(string message)
            : base(message)
        {
        }

        public BuildingsStreetException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
