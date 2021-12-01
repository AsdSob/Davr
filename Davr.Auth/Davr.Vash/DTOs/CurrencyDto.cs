
using System.ComponentModel.DataAnnotations;

namespace Davr.Vash.DTOs
{
    public class CurrencyDto
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Code is required")]
        public string Code { get; set; }

        [Required]
        public string Ccy { get; set; }

        public double Rate { get; set; }
    }
}
