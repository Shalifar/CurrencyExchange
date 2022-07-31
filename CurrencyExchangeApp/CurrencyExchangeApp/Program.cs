using CurrencyExchangeApp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
        services
            .AddScoped<IExchangeRatesClient, ExchangeRatesClient>()
            .AddScoped<ICurrencyExchangeService, CurrencyExchangeService>())
    .Build();

using IServiceScope serviceScope = host.Services.CreateScope();
IServiceProvider provider = serviceScope.ServiceProvider;
var exchangeService = provider.GetService<ICurrencyExchangeService>();

Console.WriteLine("Usage: <currency pair> <amount to exchange>");
while (true)
{
    var input = Console.ReadLine();
    var arguments = input?.Split(' ');
    if (arguments?.Length != 2)
    {
        Console.WriteLine("Invalid parameters");
        continue;
    }

    //I could validate every possible input mistake user can make here,
    //but I just don't see the point of doing that in test exercise (e.g. negative amount, currency separator).
    var currencyPair = arguments[0].Split('/');
    var exchangeResult = exchangeService.Convert(currencyPair[0], currencyPair[1], Convert.ToDecimal(arguments[1]));
    if (!exchangeResult.IsSuccess)
    {
        Console.WriteLine(exchangeResult.ErrorMessage);
        continue;
    }

    Console.WriteLine($"{exchangeResult.Result}");
}

