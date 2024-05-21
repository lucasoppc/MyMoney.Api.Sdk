using System.Text;
using System.Text.Json;
using MyMoney.Api.Sdk.Api.Common;
using MyMoney.Api.Sdk.Api.Extensions;
using MyMoney.Api.Sdk.Api.Users.Requests;
using MyMoney.Api.Sdk.Api.Users.Responses;

namespace MyMoney.Api.Sdk.Api.Users;

public class Users(IHttpClientFactory clientFactory)
{
    private readonly HttpClient _httpClient = clientFactory.CreateClient(Constants.MyMoneyApiHttpClientName);

    public async Task<ApiResponse<CreateUserResponse>> CreateUser(CreateUserRequest createUserRequest)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "api/v1.0/users")
        {
            Content = new StringContent(JsonSerializer.Serialize(createUserRequest), Encoding.UTF8, "application/json")
        };
        var result = await _httpClient.PostAsync(request.RequestUri, request.Content);
        return await result.ToApiResult<CreateUserResponse>();
    }
    
    public async Task<ApiResponse<LoginResponse>> UserLogin(string email, string password)
    {
        var body = new
        {
            email,
            password
        };
        
        var request = new HttpRequestMessage(HttpMethod.Post, "api/v1.0/users/login")
        {
            Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json")
        };
        
        var result = await _httpClient.PostAsync(request.RequestUri, request.Content);
        
        return await result.ToApiResult<LoginResponse>();
    }
}