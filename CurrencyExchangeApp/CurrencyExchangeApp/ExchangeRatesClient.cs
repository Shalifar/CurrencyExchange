namespace CurrencyExchangeApp
{
    public interface IExchangeRatesClient
    {
        Dictionary<string, decimal> GetExchangeRates();
    }

    public class ExchangeRatesClient : IExchangeRatesClient
    {
        public Dictionary<string, decimal> GetExchangeRates()
        {
            return new Dictionary<string, decimal>()
            {
                { "USD", 663.11M },
                { "EUR", 743.94M },
                { "GBP", 852.85M },
                { "SEK", 76.10M },
                { "NOK", 78.40M },
                { "CHF", 683.58M },
                { "JPY", 5.9740M },
                { "DKK", 100M },
            };
        }
    }
}
