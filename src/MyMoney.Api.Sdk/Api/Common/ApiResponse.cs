using System.Net;

namespace MyMoney.Api.Sdk.Api.Common;

public class ApiResponse<T>
{
    public HttpStatusCode StatusCode { get; set; }
    public string ErrorMessage { get; set; }
    public T? Data { get; set; }
}