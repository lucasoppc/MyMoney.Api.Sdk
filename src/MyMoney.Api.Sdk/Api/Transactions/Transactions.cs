using System.Text;
using System.Text.Json;
using MyMoney.Api.Sdk.Api.Common;
using MyMoney.Api.Sdk.Api.Extensions;
using MyMoney.Api.Sdk.Api.Transactions.Requests;
using MyMoney.Api.Sdk.Api.Transactions.Responses;

namespace MyMoney.Api.Sdk.Api.Transactions;

public class Transactions(IHttpClientFactory factory)
{
    private readonly HttpClient _httpClient = factory.CreateClient(Constants.MyMoneyApiHttpClientName);

    public async Task<ApiResponse<TransactionResponseList>> GetAccountTransactions(string userToken, string accountId)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"api/v1.0/transactions?AccountId={accountId}");
        request.Headers.Add("Authorization", $"Bearer {userToken}");
        var response = await _httpClient.SendAsync(request);
        return await response.ToApiResult<TransactionResponseList>();
    }
    
    public async Task<ApiResponse<TransactionResponse>> PostTransaction(string userToken, CreateTransactionRequest createTransactionRequest)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "api/v1.0/transactions")
        {
            Content = new StringContent(JsonSerializer.Serialize(createTransactionRequest), Encoding.UTF8, "application/json")
        };
        request.Headers.Add("Authorization", $"Bearer {userToken}");
        var response = await _httpClient.SendAsync(request);
        return await response.ToApiResult<TransactionResponse>();
    }
    
    public async Task<ApiResponse<TransactionResponse>>? TransferToAccount(string userToken, TransferToAccountRequest transferToAccountRequest)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "api/v1.0/transactions/transfer")
        {
            Content = new StringContent(JsonSerializer.Serialize(transferToAccountRequest), Encoding.UTF8, "application/json")
        };
        request.Headers.Add("Authorization", $"Bearer {userToken}");
        var response = await _httpClient.SendAsync(request);
        return await response.ToApiResult<TransactionResponse>();
    }
}