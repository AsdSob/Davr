using System.Collections.Generic;
using Davr.Vash.Entities.Abstracts;

namespace Davr.Vash.Entities
{
    public class DocumentType : EntityBase
    {
        public string Name { get; set; }

        public ICollection<Client> Clients { get; set; }

    }
}
