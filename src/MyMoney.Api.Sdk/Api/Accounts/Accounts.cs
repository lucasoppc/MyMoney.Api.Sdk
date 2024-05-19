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
}