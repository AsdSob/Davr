using System.ComponentModel.DataAnnotations;
using Davr.Vash.Entities;

namespace Davr.Vash.Models.Users
{
    public class RegisterRequest
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Username { get; set; }        
        
        public string MiddleName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public Role Role { get; set; }
    }
}