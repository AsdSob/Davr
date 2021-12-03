using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Davr.Gumon.Entities.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace Davr.Gumon.Entities
{
    [Index(nameof(Name), nameof(Code), IsUnique = true)]
    public class Currency : EntityBase
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Ccy { get; set; }

        public ICollection<TransactionSuspicious> TransactionSuspiciouses { get; set; }


    }
}
