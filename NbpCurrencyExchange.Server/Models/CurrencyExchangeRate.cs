namespace NbpCurrencyExchange.Server.Models
{
    public class CurrencyExchangeRate
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Rate { get; set; }
        public DateOnly EffectiveDate { get; set; }
    }
}
