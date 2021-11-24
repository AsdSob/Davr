using System;
using Davr.Vash.Entities.Abstracts;

namespace Davr.Vash.Entities
{
    public class ExchangeTransaction: EntityBase
    {
        public DateTime EntryDate { get; set; }

        public bool IsCash { get; set; }
        public int CardNumber { get; set; }
        public string Comment { get; set; }
        public bool IsBuying { get; set; }
        public double Amount { get; set; }
        public string SourceOfOrigin { get; set; }

        public int CurrencyId { get; set; }
        public int CurrencyRateId { get; set; }
        public int ClientId { get; set; }


        public int UserId { get; set; }

        public virtual Currency Currency { get; set; }
        public virtual CurrencyRate CurrencyRate { get; set; }
        public virtual Client Client { get; set; }
    }
}
