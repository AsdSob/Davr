using System.ComponentModel.DataAnnotations;

namespace Davr.Vash.DTOs
{
    public class BranchDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
    }
}
