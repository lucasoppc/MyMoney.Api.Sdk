using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using MyMoney.Api.Sdk.Api.Common;

namespace MyMoney.Api.Sdk.Api.Extensions;

public static class HttpResponseMessageExtensions
{
    public static async Task<ApiResponse<T>> ToApiResult<T>(this HttpResponseMessage response)
    {
        var jsonOptions = new JsonSerializerOptions
        { 
            PropertyNameCaseInsensitive = true, 
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase 
        };
        var apiResponse = new ApiResponse<T>();
        
        var content = await response.Content.ReadAsStringAsync();
        
        if (response.IsSuccessStatusCode)
        {
            apiResponse.Data = JsonSerializer.Deserialize<T>(content, jsonOptions);
        }
        else
        {
            if (!string.IsNullOrEmpty(content))
            {
                var jsonDocument = JsonSerializer.Deserialize<JsonElement>(content, jsonOptions);
                var existErrorMessage = jsonDocument.TryGetProperty("error", out var error);
                if (existErrorMessage)
                {
                    apiResponse.ErrorMessage = error.Deserialize<string>();
                }

                var errorsJson = jsonDocument.TryGetProperty("errors", out var errorsJsonElement);
                if (errorsJson)
                {
                    apiResponse.ErrorList = errorsJsonElement.Deserialize<List<string>>();
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