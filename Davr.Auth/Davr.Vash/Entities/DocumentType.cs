using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Davr.Vash.Entities.Abstracts;

namespace Davr.Vash.Entities
{
    public class DocumentType : EntityBase
    {
        [Required]
        public string Name { get; set; }

        public ICollection<Client> Clients { get; set; }

    }
}
