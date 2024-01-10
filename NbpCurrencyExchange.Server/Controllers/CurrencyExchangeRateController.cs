using Microsoft.AspNetCore.Mvc;
using NbpCurrencyExchange.Server.Models;
using NbpCurrencyExchange.Server.Services;

namespace NbpCurrencyExchange.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyExchangeRateController : ControllerBase
    {
        private readonly ICurrencyExchangeRateService currencyExchangeRateService;

        public CurrencyExchangeRateController(ICurrencyExchangeRateService currencyExchangeRateService)
        {
            this.currencyExchangeRateService = currencyExchangeRateService;
        }

        [HttpGet]
        public async Task<IEnumerable<CurrencyExchangeRate>> GetRates()
        {
            IEnumerable<CurrencyExchangeRate> result = await currencyExchangeRateService.GetCurrentExchangeRates();
            return result;

        }
    }
}
