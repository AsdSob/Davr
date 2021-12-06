using System;
using Davr.Gumon.Entities.Abstracts;

namespace Davr.Gumon.Entities
{
    public class TransactionSuspicious : EntityBase
    {
        public string Number { get; set; }
        public DateTime EntryDate { get; set; }
        public string CounterpartyName { get; set; }
        public string CounterpartyAccount { get; set; }
        public string ClientName { get; set; }
        public string ClientAccount { get; set; }

        public string TransactionDetails { get; set; }
        public DateTime TransactionDate { get; set; }
        public double TransactionAmount { get; set; }
        public string Comment { get; set; }

        public int TransactionStatusId { get; set; }
        public int OperationCriteriaId { get; set; }
        public int CurrencyId { get; set; }
        public int UserId { get; set; }
        public int BranchId { get; set; }


        public TransactionStatus TransactionStatus { get; set; }
        public OperationCriteria OperationCriteria { get; set; }
        public Currency Currency { get; set; }
        public Branch Branch { get; set; }
        public User User { get; set; }
    }
}
