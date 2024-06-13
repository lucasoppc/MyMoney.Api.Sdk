using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyMoney.Api.Sdk.Api;
using MyMoney.Api.Sdk.Api.Accounts;
using MyMoney.Api.Sdk.Api.Common;
using MyMoney.Api.Sdk.Api.Transactions;
using MyMoney.Api.Sdk.Api.Users;

namespace MyMoney.Api.Sdk.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMyMoneyApiSdk(this IServiceCollection services, MyMoneyApiSdkOptions options)
    {
        var config = services.BuildServiceProvider().GetService<IConfiguration>();
        services.AddHttpClient(Constants.MyMoneyApiHttpClientName, client =>
        {
            client.BaseAddress = new Uri(options.BaseUrl);
            client.Timeout = TimeSpan.FromSeconds(options.TimeoutInSeconds);
        });
        services.AddSingleton<Accounts>();
        services.AddSingleton<Users>();
        services.AddSingleton<Transactions>();
        services.AddSingleton<MyMoneyApiClient>();

        return services;
    }
}