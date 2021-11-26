using System.ComponentModel.DataAnnotations;

namespace Davr.Vash.DTOs.Users
{
    public class AuthenticateRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}