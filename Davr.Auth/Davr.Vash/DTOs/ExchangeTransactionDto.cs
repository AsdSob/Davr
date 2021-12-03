using System;
using System.ComponentModel.DataAnnotations;
using Davr.Vash.Entities;

namespace Davr.Vash.DTOs
{
    public class ExchangeTransactionDto
    {
        public int Id { get; set; }

        public DateTime EntryDate { get; set; }
        public bool IsCash { get; set; }
        public string CardNumber { get; set; }
        public string Comment { get; set; }
        public bool IsBuying { get; set; }
        public double Amount { get; set; }
        public string SourceOfOrigin { get; set; }
        public double CurrencyRate { get; set; }

        public int CurrencyId { get; set; }
        public int BranchId { get; set; }
        public int UserId { get; set; }




        public int ClientId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "SureName is required")]
        public string SurName { get; set; }

        public string MiddleSurName { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public string BirthPlace { get; set; }

        [Required]
        public string DocumentSeries { get; set; }

        [Required]
        public string DocumentNumber { get; set; }

        [Required]
        public string DocumentAuthority { get; set; }

        [Required]
        public DateTime DocumentIssueDate { get; set; }

        [Required]
        public string Registration { get; set; }

        [Required]
        public int DocumentTypeId { get; set; }

        [Required]
        public int CitizenId { get; set; }


        public virtual UserDto User { get; set; }
        public virtual CurrencyDto Currency { get; set; }
        public virtual BranchDto Branch { get; set; }
        public virtual CitizenDto Citizen { get; set; }
        public virtual DocumentTypeDto DocumentType { get; set; }
    }
}
