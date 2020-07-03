using System.ComponentModel.DataAnnotations;

namespace AdvertApi.DTOs
{
    public class LoginRequest
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
