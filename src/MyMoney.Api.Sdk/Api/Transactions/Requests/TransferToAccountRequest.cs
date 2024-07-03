namespace MyMoney.Api.Sdk.Api.Transactions.Requests;

public class TransferToAccountRequest
{
    public string FromAccountId { get; set; }
    
    public string ToAccountId { get; set; }
    
    public decimal Amount { get; set; }
}