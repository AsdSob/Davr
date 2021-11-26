
using Davr.Vash.Entities;

namespace Davr.Vash.DTOs
{
    public class CurrencyRateDto
    {
        public int Id { get; set; }
        public double Rate { get; set; }
        public int CurrencyId { get; set; }

        public virtual CurrencyDto Currency { get; set; }
    }
}
