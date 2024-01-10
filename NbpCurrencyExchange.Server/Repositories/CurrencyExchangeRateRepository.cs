using Microsoft.EntityFrameworkCore;
using NbpCurrencyExchange.Server.Data;
using NbpCurrencyExchange.Server.Models;

namespace NbpCurrencyExchange.Server.Repositories
{
    public interface ICurrencyExchangeRepository
    {
        public Task<List<CurrencyExchangeRate>> AddMany(List<CurrencyExchangeRate> rates);
        public Task<bool> CheckIfUpToDate(DateOnly date);
    }
    public class CurrencyExchangeRateRepository : ICurrencyExchangeRepository
    {
        private readonly CurrencyExchangeDbContext _dbContext;

        public CurrencyExchangeRateRepository(CurrencyExchangeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<CurrencyExchangeRate>> AddMany(List<CurrencyExchangeRate> rates)
        {
            await _dbContext.CurrencyExchangeRates.AddRangeAsync(rates);
            await _dbContext.SaveChangesAsync();
            return rates;
        }

        public async Task<bool> CheckIfUpToDate(DateOnly date)
        {
            return await _dbContext.CurrencyExchangeRates.AnyAsync(c => c.EffectiveDate == date);
        }
    }
}
