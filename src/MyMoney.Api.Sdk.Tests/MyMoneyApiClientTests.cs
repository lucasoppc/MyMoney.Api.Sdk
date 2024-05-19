using System.Net;
using Microsoft.Extensions.DependencyInjection;
using MyMoney.Api.Sdk.Api;
using MyMoney.Api.Sdk.Configuration;
using Xunit;

namespace MyMoney.Api.Sdk.Tests;

public class MyMoneyApiClientTests
{
    private readonly IServiceCollection _serviceCollection = new ServiceCollection();
    private MyMoneyApiClient? _client;
    private string _token = string.Empty;

    public MyMoneyApiClientTests()
    {
        ConfigureServices();
        var serviceProvider = _serviceCollection.BuildServiceProvider();
        _client = serviceProvider.GetService<MyMoneyApiClient>();
    }

    [Fact]
    public async Task TestUserLoginSuccessful()
    {
        var response = await _client.Users.UserLogin(
            "lucas@test.com",
            "");
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(response.Data);
        Assert.NotNull(response.Data?.token);
        Assert.NotEmpty(response.Data?.token);

        _token = response.Data.token;
    }
    
    [Fact]
    public async Task TestGetUserAccountsUnauthorized()
    {
        var response = await _client.Accounts.GetUserAccounts(
            "userToken");
        
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task TestGetUserAccountsOk()
    {
        var response = await _client.Accounts.GetUserAccounts(
            _token);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(response.Data);
        Assert.True(response.Data?.Accounts?.Count > 0);
    }
    
    private void ConfigureServices()
    {
        var options = new MyMoneyApiSdkOptions()
        {
            BaseUrl = "https://oppc-my-money-api.azurewebsites.net",
            TimeoutInSeconds = 30
        };
        _serviceCollection.AddMyMoneyApiSdk(options);
    }
}