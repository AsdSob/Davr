using Davr.Gumon.Entities;

namespace Davr.Gumon.DTOs.Users
{
    public class UpdateRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public int BranchId { get; set; }
        public string MiddleName { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}