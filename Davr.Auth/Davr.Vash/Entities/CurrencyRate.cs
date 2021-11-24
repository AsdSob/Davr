using System.Collections.Generic;
using Davr.Vash.Entities.Abstracts;

namespace Davr.Vash.Entities
{
    public class CurrencyRate : EntityBase
    {
        public double Rate { get; set; }

        public int CurrencyId { get; set; }

        public virtual Currency Currency { get; set; }
        public ICollection<ExchangeTransaction> ExchangeTransactions { get; set; }

        //TODO: Date of rate need? or keep only valid 
    }
}
