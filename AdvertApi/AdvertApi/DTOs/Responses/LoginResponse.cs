using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertApi.DTOs.Responses
{
    public class LoginResponse
    {
        public string AccesToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
