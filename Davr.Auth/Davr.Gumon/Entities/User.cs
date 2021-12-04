using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Davr.Gumon.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public Role Role { get; set; }
        
        
        public int BranchId { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; }

        public virtual Branch Branch { get; set; }

        public ICollection<TransactionSuspicious> TransactionSuspiciouses { get; set; }

    }
}