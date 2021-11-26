using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Davr.Vash.Entities.Abstracts;

namespace Davr.Vash.Entities
{
    public class Branch : EntityBase
    {
        [Required]
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }
        public ICollection<ExchangeTransaction> ExchangeTransactions { get; set; }

    }
}
