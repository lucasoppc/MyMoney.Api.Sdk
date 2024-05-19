using System.Text.Json;
using System.Text.Json.Serialization;
using MyMoney.Api.Sdk.Api.Common;

namespace MyMoney.Api.Sdk.Api.Extensions;

public static class HttpResponseMessageExtensions
{
    public static async Task<ApiResponse<T>> ToApiResult<T>(this HttpResponseMessage response)
    {
        var apiResponse = new ApiResponse<T>();
        
        var content = await response.Content.ReadAsStringAsync();
        
        if (response.IsSuccessStatusCode)
        {
            apiResponse.Data = JsonSerializer.Deserialize<T>(content);
        }
        else
        {
            if (!string.IsNullOrEmpty(content))
            {
                var jsonDocument = JsonSerializer.Deserialize<JsonElement>(content);
                var existErrorMessage = jsonDocument.TryGetProperty("error", out var error);
                if (existErrorMessage)
                {
                    apiResponse.ErrorMessage = error.ToString();
                }
            }
            else
            {
                apiResponse.ErrorMessage = "Unexpected status code";
            }
        }

        apiResponse.StatusCode = response.StatusCode;
        
        return apiResponse;
    }
}