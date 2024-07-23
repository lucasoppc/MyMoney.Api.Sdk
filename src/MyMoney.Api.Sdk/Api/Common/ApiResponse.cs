using System.Net;

namespace MyMoney.Api.Sdk.Api.Common;

public class ApiResponse<T>
{
    public HttpStatusCode StatusCode { get; set; }
    public bool IsSuccessfulStatusCode => (int)StatusCode >= 200 && (int)StatusCode <= 299;
    public string? ErrorMessage { get; set; }
    public List<string>? ErrorList { get; set; }
    public T? Data { get; set; }
}