namespace MyMoney.Api.Sdk.Api.Accounts.Requests;

public class CreateAccountRequest
{
    public string Name { get; set; }
    public string Currency { get; set; }
    public string BankAccount { get; set; }
}