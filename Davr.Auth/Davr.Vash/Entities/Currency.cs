using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Davr.Vash.Entities.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace Davr.Vash.Entities
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

        public double Rate { get; set; }

        public ICollection<ExchangeTransaction> ExchangeTransactions { get; set; }
    }
}
