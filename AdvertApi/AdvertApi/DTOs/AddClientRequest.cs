using System.ComponentModel.DataAnnotations;

namespace AdvertApi.DTOs
{
    public class AddClientRequest
    {
        [Required]
        public int IdClient { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
