using AutoFixture;
using CurrencyExchangeApp;
using Moq.AutoMock;

namespace CurrencyExchangeAppTests
{
    public class CurrencyExchangeServiceTests
    {
        private static object[] TestCases =
        {
            new object[] { new Dictionary<string, decimal>() 
            { 
                {"EUR", 10M }, { "DKK", 200M }
            }, "EUR", "DKK", 20M, 400M },

            new object[] { new Dictionary<string, decimal>()
            {
                {"EUR", 10M }, { "DKK", 200M }
            }, "EUR", "EUR", 30M, 30M },
        };

        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void Convert_ShouldReturn_CorrectAmount(
            Dictionary<string, decimal> exchangeRates, 
            string mainCurrency, 
            string moneyCurrency, 
            decimal inputAmount, 
            decimal outputAmount)
        {
            //Arrange
            var container = new AutoMocker();

            var ratesClient = container.GetMock<IExchangeRatesClient>().Setup(x => x.GetExchangeRates()).Returns(exchangeRates);

            //Act
            var exchangeService = container.CreateInstance<CurrencyExchangeService>();
            var result = exchangeService.Convert(mainCurrency, moneyCurrency, inputAmount);

            //Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.That(outputAmount, Is.EqualTo(result.Result));
        }
    }
}