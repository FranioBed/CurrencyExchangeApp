using NbpCurrencyExchange.Server.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using System.Linq;
using NbpCurrencyExchange.Server.Repositories;

namespace NbpCurrencyExchange.Server.Services
{
    public interface ICurrencyExchangeRateService
    {
        public Task<List<CurrencyExchangeRate>> GetCurrentExchangeRates();
    }

    public class CurrencyExchangeRateService : ICurrencyExchangeRateService
    {
        private readonly ICurrencyExchangeRepository _currencyExchangeRepository;

        public CurrencyExchangeRateService(ICurrencyExchangeRepository currencyExchangeRepository)
        {
            _currencyExchangeRepository = currencyExchangeRepository;
        }

        public async Task<List<CurrencyExchangeRate>> GetCurrentExchangeRates()
        {
            List<NbpApiResponseRootObj> data = new List<NbpApiResponseRootObj>();
            List<CurrencyExchangeRate> rates = new List<CurrencyExchangeRate>();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await httpClient.GetAsync("http://api.nbp.pl/api/exchangerates/tables/A/");


                if (response.IsSuccessStatusCode) 
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    data.AddRange(JsonConvert.DeserializeObject<List<NbpApiResponseRootObj>>(responseBody));
                    rates.AddRange(data.SelectMany(x => x.Rates,
                        (x, rate) => new CurrencyExchangeRate
                        {
                            Code = rate.Code,
                            Name = rate.Currency,
                            Rate = rate.Mid,
                            EffectiveDate = DateOnly.Parse(x.EffectiveDate)
                        })
                        .ToList());
                }
                else
                {
                    throw new Exception("bad request");
                }

                if (!(await _currencyExchangeRepository.CheckIfUpToDate(rates.First().EffectiveDate)))
                {
                    await _currencyExchangeRepository.AddMany(rates);
                }

                return rates;
            }
        }
    }

}

public class Rate
{
    [JsonPropertyName("currency")]
    public string Currency { get; set; }
    [JsonPropertyName("code")]
    public string Code { get; set; }
    [JsonPropertyName("mid")]
    public decimal Mid { get; set; }
}

public class NbpApiResponseRootObj
{
    [JsonPropertyName("table")]
    public string Table { get; set; }
    [JsonPropertyName("no")]
    public string No { get; set; }
    [JsonPropertyName("effectiveDate")]
    public string EffectiveDate { get; set; }
    [JsonPropertyName("rates")]
    public List<Rate> Rates { get; set; }
}