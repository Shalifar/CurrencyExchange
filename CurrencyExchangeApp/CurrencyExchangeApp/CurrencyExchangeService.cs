namespace CurrencyExchangeApp
{
    public interface ICurrencyExchangeService
    {
        ResultModel Convert(string mainCurrency, string moneyCurrency, decimal amount);
    }

    public class CurrencyExchangeService : ICurrencyExchangeService
    {
        private readonly IExchangeRatesClient _exchangeRatesClient;

        public CurrencyExchangeService(IExchangeRatesClient exchangeRatesClient)
        {
            _exchangeRatesClient = exchangeRatesClient;
        }

        public ResultModel Convert(string mainCurrency, string moneyCurrency, decimal amount)
        {
            var exchangeRates = _exchangeRatesClient.GetExchangeRates();

            if (!exchangeRates.TryGetValue(mainCurrency, out var mainCurrencyRatio))
            {
                return ResultModel.Failed($"{mainCurrency} currency not supported");
            }

            if (!exchangeRates.TryGetValue(moneyCurrency, out var moneyCurrencyRatio))
            {
                return ResultModel.Failed($"{moneyCurrency} currency not supported");
            }

            var result = Math.Round(amount / mainCurrencyRatio * moneyCurrencyRatio, 4);
            return ResultModel.Success(result);
        }
    }
}
