using Microsoft.EntityFrameworkCore;
using NbpCurrencyExchange.Server.Models;

namespace NbpCurrencyExchange.Server.Data
{
    public class CurrencyExchangeDbContext : DbContext
    {
        public CurrencyExchangeDbContext(DbContextOptions<CurrencyExchangeDbContext> dbContextOptions) : base(dbContextOptions) { }
        public DbSet<CurrencyExchangeRate> CurrencyExchangeRates { get; set; }
    }
}
