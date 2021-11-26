
using System.ComponentModel.DataAnnotations;

namespace Davr.Vash.DTOs
{
    public class CitizenDto
    {

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
    }
}
