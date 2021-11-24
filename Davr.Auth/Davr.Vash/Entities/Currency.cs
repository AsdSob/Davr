using System.Collections.Generic;
using Davr.Vash.Entities.Abstracts;

namespace Davr.Vash.Entities
{
    public class Currency : EntityBase
    {
        public string Name { get; set; }

        public ICollection<CurrencyRate> CurrencyRates { get; set; }
        public ICollection<ExchangeTransaction> ExchangeTransactions { get; set; }
    }
}
