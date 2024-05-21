namespace MyMoney.Api.Sdk.Api.Users.Requests;

public class CreateUserRequest
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string DefaultCurrency { get; set; }
}