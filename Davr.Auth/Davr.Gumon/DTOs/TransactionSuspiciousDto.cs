using System;

namespace Davr.Gumon.DTOs
{
    public class TransactionSuspiciousDto
    {
        public int Id { get; set; }

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


        public TransactionStatusDto TransactionStatus { get; set; }
        public OperationCriteriaDto OperationCriteria { get; set; }
        public CurrencyDto Currency { get; set; }
        public BranchDto BranchDto { get; set; }
        public UserDto User { get; set; }
    }
}
