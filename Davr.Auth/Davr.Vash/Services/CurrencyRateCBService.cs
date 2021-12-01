using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Davr.Vash.DataAccess;

namespace Davr.Vash.Services
{
    public class CurrencyRateCBService : ICurrencyRateCBService
    {
        private string CBUrl = $"https://cbu.uz/ru/arkhiv-kursov-valyut/json/all/{DateTime.Now.Date}/";
        private List<CBRate> rates;
        private readonly IDataAccessProvider _dbContext;
        private readonly ILoggerManager _logger;
        
        public CurrencyRateCBService(IDataAccessProvider dbContext, ILoggerManager logger)
        {
            _dbContext = dbContext;
            _logger = logger;
            GetValidCurrencies();
        } 


        public async Task GetValidCurrencies()
        {
            string json = (new WebClient()).DownloadString(CBUrl);
            rates = JsonSerializer.Deserialize<List<CBRate>>(json);

            for (int i = 0; i < 10; i++)
            {
                _logger.LogInfo(rates[i].Code + " | "+rates[i].Ccy + " | "+ rates[i].Rate);
            }


            //using (var httpClient = new HttpClient())
            //{
            //    var httpResponseMessage = await httpClient.GetAsync(CBUrl);
            //    var content = await httpResponseMessage.Content.ReadAsStringAsync();

            //    _logger.LogError("json from httpClient\n" + content);

            //    rates = JsonSerializer.Deserialize<List<CBRate>>(content);
            //}


            _logger.LogInfo("Setting currency rate");

            var currencies = _dbContext._context.Currencies;
            foreach (var currency in currencies)
            {
                var rate = rates.FirstOrDefault(x => x.Code == currency.Code);

                if(rate == null)continue;

                currency.Rate = Convert.ToDouble(rate.Rate);

                _logger.LogInfo(currency.Code + " | " + currency.Ccy + " | " + currency.Rate);

            }

            _dbContext._context.SaveChanges();
        }

        public async Task<double> GetCurrency(string code)
        {
            var cb = rates.FirstOrDefault(x => x.Code == code);
            _logger.LogInfo($"start code={code}    entity= {cb}  {cb.Code} {cb.Ccy}  {cb.Rate}");

            var rate = cb.RateD;
            _logger.LogInfo("rate in CB" + rate);

            return rate;
        }
    }

    class CBRate
    {
        public string Code { get; set; }
        public string Ccy { get; set; }
        public string Rate { get; set; }
        public double RateD { get; set; }
    }
}
