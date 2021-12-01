using System.Collections.Generic;
using System.Threading.Tasks;
using Davr.Vash.DTOs;
using Davr.Vash.Entities;

namespace Davr.Vash.Services
{
    public interface ICurrencyRateCBService
    {
        Task<double> GetCurrency(string code);
    }
}
