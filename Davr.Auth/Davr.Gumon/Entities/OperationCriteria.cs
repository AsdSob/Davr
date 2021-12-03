using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Davr.Gumon.Entities.Abstracts;

namespace Davr.Gumon.Entities
{
    public class OperationCriteria : EntityBase
    {
        public string Name { get; set; }

        [Required]
        public string Code { get; set; }


        public ICollection<TransactionSuspicious> TransactionSuspiciouses { get; set; }

    }
}
