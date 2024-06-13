
namespace MyMoney.Api.Sdk.Api;

public class MyMoneyApiClient(Accounts.Accounts  accounts,
    Users.Users users,
    Transactions.Transactions transactions)
{
    public Accounts.Accounts Accounts => accounts;
    public Users.Users Users => users;
    public Transactions.Transactions Transactions => transactions;
}