
using System.ComponentModel.DataAnnotations;
using Davr.Vash.Entities;

namespace Davr.Vash.DTOs
{
    public class CurrencyRateDto
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Rate is required")]
        public double Rate { get; set; }

        [Required(ErrorMessage = "Currency Id is required")]
        public int CurrencyId { get; set; }

        public virtual CurrencyDto Currency { get; set; }
    }
}
