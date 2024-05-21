namespace MyMoney.Api.Sdk.Api.Transactions.Requests;

public class CreateTransactionRequest
{
    public string AccountId { get; set; }
    public string Description { get; set; }
    public string Currency { get; set; }
    public decimal Amount { get; set; }
    public string Date { get; set; }
}