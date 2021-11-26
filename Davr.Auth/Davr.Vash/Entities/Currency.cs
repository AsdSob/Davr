using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Davr.Vash.Entities.Abstracts;

namespace Davr.Vash.Entities
{
    public class Currency : EntityBase
    {
        [Required]
        public string Name { get; set; }

        public virtual CurrencyRate CurrencyRate { get; set; }
        public ICollection<ExchangeTransaction> ExchangeTransactions { get; set; }
    }
}
