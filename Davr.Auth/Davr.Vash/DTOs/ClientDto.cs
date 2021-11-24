using System;

namespace Davr.Vash.DTOs
{
    public class ClientDto
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
    }
}
