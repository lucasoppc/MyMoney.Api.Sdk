using System.Net;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.Extensions.DependencyInjection;
using MyMoney.Api.Sdk.Api;
using MyMoney.Api.Sdk.Api.Accounts.Requests;
using MyMoney.Api.Sdk.Api.Accounts.Responses;
using MyMoney.Api.Sdk.Api.Transactions.Responses;
using MyMoney.Api.Sdk.Api.Users.Requests;
using MyMoney.Api.Sdk.Api.Users.Responses;
using MyMoney.Api.Sdk.Configuration;
using Xunit;

namespace MyMoney.Api.Sdk.Tests;

public class MyMoneyApiClientTests
{
    private readonly IServiceCollection _serviceCollection = new ServiceCollection();
    private MyMoneyApiClient? _client;
    private TestState _testState = new TestState();

    public MyMoneyApiClientTests()
    {
        ConfigureServices();
        var serviceProvider = _serviceCollection.BuildServiceProvider();
        _client = serviceProvider.GetService<MyMoneyApiClient>();
    }
    
    class TestState
    {
        public string UserEmail = $"jdoe@test.com";
        public string UserPassword = "Qwertyui1!";
    }

    [Fact]
    public async Task CreateUserSuccessfulTest()
    {
        var request = new CreateUserRequest()
        {
            Name = "Harness Test User",
            Password = "Qwertyui1!",
            Email = $"{DateTime.Now}@test.com",
            DefaultCurrency = "USD"
        };
        
        var response = await _client.Users.CreateUser(request);
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(response.Data);
        Assert.NotNull(response.Data?.Token);
        Assert.Equal(request.Name, response.Data.Name);
        Assert.Equal(request.Email, response.Data.Email);
        Assert.Equal(request.DefaultCurrency, response.Data.DefaultCurrency);
    }
    
    [Fact]
    public async Task CreateUserAlreadyExistsFailsTest()
    {
        var request = new CreateUserRequest()
        {
            Name = "Harness Test User",
            Password = _testState.UserPassword,
            Email = _testState.UserEmail,
            DefaultCurrency = "USD"
        };
        
        var response = await _client.Users.CreateUser(request);
        
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Null(response.Data);
        Assert.NotEmpty(response.ErrorMessage);
    }
    
    [Fact]
    public async Task TestUserLoginSuccessful()
    {
        var response = await _client.Users.UserLogin(
            _testState.UserEmail,
            _testState.UserPassword);
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(response.Data);
        Assert.NotNull(response.Data?.token);
        Assert.NotEmpty(response.Data?.token);
    }

    [Fact]
    public async Task CreateAccountUnauthorizedFails()
    {
        var response = await _client.Accounts.PostAccount("invalid token", new CreateAccountRequest
        {
            Name = "My salary",
            Currency = "USD",
        });
        
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        Assert.Null(response.Data);
    }
    
    [Fact]
    public async Task CreateAccountSuccess()
    {
        var loginResponse = await _client.Users.UserLogin(
            _testState.UserEmail,
            _testState.UserPassword);
        
        var response = await _client.Accounts.PostAccount(loginResponse.Data.token, new CreateAccountRequest
        {
            Name = "My salary",
            Currency = "USD",
        });
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(response.Data);
        Assert.NotNull(response.Data?.Id);
        Assert.Equal("My salary", response.Data.Name);
        Assert.Equal("USD", response.Data.Currency);
    }
    
    [Fact]
    public async Task TestGetUserAccountsUnauthorizedFails()
    {
        var response = await _client.Accounts.GetUserAccounts(
            "userToken");
        
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task TestGetUserAccountsOk()
    {
        var loginResponse = await _client.Users.UserLogin(
            _testState.UserEmail,
            _testState.UserPassword);
        
        var response = await _client.Accounts.GetUserAccounts(
            loginResponse.Data.token);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(response.Data);
        Assert.True(response.Data?.Accounts?.Count > 0);
    }
    
    private void ConfigureServices()
    {
        const string local = "https://localhost:7041";
        const string azure = "https://oppc-my-money-api.azurewebsites.net";
        var options = new MyMoneyApiSdkOptions()
        {
            BaseUrl = local,
            TimeoutInSeconds = 30
        };
        _serviceCollection.AddMyMoneyApiSdk(options);
    }
}