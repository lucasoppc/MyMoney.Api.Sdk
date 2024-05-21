namespace MyMoney.Api.Sdk.Api.Users.Responses;

public class CreateUserResponse
{
    public string UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string DefaultCurrency { get; set; }
    public string Token { get; set; }
}