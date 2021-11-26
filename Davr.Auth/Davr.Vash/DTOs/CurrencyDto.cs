
using System.ComponentModel.DataAnnotations;

namespace Davr.Vash.DTOs
{
    public class CurrencyDto
    {

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
    }
}
