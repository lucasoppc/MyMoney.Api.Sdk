using MyMoney.Api.Sdk.Api.Common;

namespace MyMoney.Api.Sdk.Api.Accounts.Responses;

public class AccountResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string UserId { get; set; }
    public string Currency { get; set; }
    public bool IsDeleted { get; set; }
    public decimal Amount { get; set; }
}