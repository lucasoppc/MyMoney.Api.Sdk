using System.Text;
using System.Text.Json;
using MyMoney.Api.Sdk.Api.Accounts.Requests;
using MyMoney.Api.Sdk.Api.Accounts.Responses;
using MyMoney.Api.Sdk.Api.Common;
using MyMoney.Api.Sdk.Api.Extensions;

namespace MyMoney.Api.Sdk.Api.Accounts;

public class Accounts(IHttpClientFactory factory)
{
    private readonly HttpClient _httpClient = factory.CreateClient(Constants.MyMoneyApiHttpClientName);

    public async Task<ApiResponse<AccountResponseList>> GetUserAccounts(string userToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "api/v1.0/accounts");
        request.Headers.Add("Authorization", $"Bearer {userToken}");
        var response = await _httpClient.SendAsync(request);
        return await response.ToApiResult<AccountResponseList>();
    }
    
    public async Task<ApiResponse<AccountResponse>> PostAccount(string userToken, CreateAccountRequest createAccountRequest)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "api/v1.0/accounts")
        {
            Content = new StringContent(JsonSerializer.Serialize(createAccountRequest), Encoding.UTF8, "application/json")
        };
        request.Headers.Add("Authorization", $"Bearer {userToken}");
        var response = await _httpClient.SendAsync(request);
        return await response.ToApiResult<AccountResponse>();
    }
    
    public async Task<ApiResponse<AccountResponse>> UpdateAccount(string userToken, UpdateAccountRequest updateAccountRequest)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, "api/v1.0/accounts/update")
        {
            Content = new StringContent(JsonSerializer.Serialize(updateAccountRequest), Encoding.UTF8, "application/json")
        };
        request.Headers.Add("Authorization", $"Bearer {userToken}");
        var response = await _httpClient.SendAsync(request);
        return await response.ToApiResult<AccountResponse>();
    }
    
    public async Task<ApiResponse<AccountResponse>> DeleteAccount(string userToken, string accountId)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, $"api/v1.0/accounts/{accountId}");
        request.Headers.Add("Authorization", $"Bearer {userToken}");
        var response = await _httpClient.SendAsync(request);
        return await response.ToApiResult<AccountResponse>();
    }
}