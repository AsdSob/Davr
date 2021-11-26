using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Davr.Gumon.Entities.Abstracts;

namespace Davr.Gumon.Entities
{
    public class Branch : EntityBase
    {
        [Required]
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }

    }
}
