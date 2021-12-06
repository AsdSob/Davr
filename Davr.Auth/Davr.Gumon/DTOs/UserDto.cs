using Davr.Gumon.Entities;

namespace Davr.Gumon.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public Role Role { get; set; }
        
        public int? BranchId { get; set; }
        
        public virtual BranchDto Branch { get; set; }
    }
}
