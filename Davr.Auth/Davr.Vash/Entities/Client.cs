using System;
using System.Collections.Generic;
using Davr.Vash.Entities.Abstracts;

namespace Davr.Vash.Entities
{
    public class Client : EntityBase
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string MiddleSurName { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string DocumentSeries { get; set; }
        public string DocumentNumber { get; set; }
        public string DocumentAuthority { get; set; }
        public DateTime DocumentIssueDate { get; set; }
        public string Registration { get; set; }

        public int DocumentTypeId { get; set; }
        public int CitizenId { get; set; }

        public virtual DocumentType DocumentType { get; set; }
        public virtual Citizen Citizen { get; set; }
        public ICollection<ExchangeTransaction> ExchangeTransactions { get; set; }

    }
}
