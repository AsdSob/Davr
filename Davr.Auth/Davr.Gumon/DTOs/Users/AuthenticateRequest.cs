using System.ComponentModel.DataAnnotations;

namespace Davr.Gumon.DTOs.Users
{
    public class AuthenticateRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}