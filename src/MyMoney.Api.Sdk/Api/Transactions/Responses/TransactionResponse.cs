namespace MyMoney.Api.Sdk.Api.Transactions.Responses;

public class TransactionResponse
{
    public string Id { get; set; }
    public string AccountId { get; set; }
    public string UserId { get; set; }
    public string Description { get; set; }
    public string Currency { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
}