﻿using System;
using Davr.Vash.Entities;

namespace Davr.Vash.DTOs
{
    public class ExchangeTransactionDto
    {
        public int Id { get; set; }

        public DateTime EntryDate { get; set; }
        public bool IsCash { get; set; }
        public int CardNumber { get; set; }
        public string Comment { get; set; }
        public bool IsBuying { get; set; }
        public double Amount { get; set; }
        public string SourceOfOrigin { get; set; }

        public int CurrencyId { get; set; }
        public double CurrencyRate { get; set; }
        public int ClientId { get; set; }
        public int BranchId { get; set; }
        public int UserId { get; set; }


        //public virtual User User { get; set; }
        //public virtual Currency Currency { get; set; }
        //public virtual Client Client { get; set; }
        //public virtual Branch Branch { get; set; }
    }
}