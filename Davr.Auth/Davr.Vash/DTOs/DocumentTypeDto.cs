﻿
using System.ComponentModel.DataAnnotations;

namespace Davr.Vash.DTOs
{
    public class DocumentTypeDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
