using System;
using System.ComponentModel.DataAnnotations;

namespace Davr.Vash.DTOs
{
    public class ClientDto
    {
        public int Id { get; set; }

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

        public CitizenDto Citizen { get; set; }
        public DocumentTypeDto DocumentType { get; set; }
    }
}
