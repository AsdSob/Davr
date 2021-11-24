using System.Collections.Generic;
using Davr.Vash.Entities.Abstracts;

namespace Davr.Vash.Entities
{
    public class Citizen : EntityBase
    {
        public string Name { get; set; }

        public ICollection<Client> Clients { get; set; }

    }
}
