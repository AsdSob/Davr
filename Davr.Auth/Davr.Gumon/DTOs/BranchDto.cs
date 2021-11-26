using System.ComponentModel.DataAnnotations;

namespace Davr.Gumon.DTOs
{
    public class BranchDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
    }
}
