namespace MyMoney.Api.Sdk.Api.Accounts.Requests;

public class UpdateAccountRequest
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string BankAccount { get; set; }
    public string Currency { get; set; }
    public bool IsDeleted { get; set; }
}