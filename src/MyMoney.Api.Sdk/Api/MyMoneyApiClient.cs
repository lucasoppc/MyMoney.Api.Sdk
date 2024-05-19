
namespace MyMoney.Api.Sdk.Api;

public class MyMoneyApiClient(Accounts.Accounts  accounts,
    Users.Users users)
{
    public Accounts.Accounts Accounts => accounts;
    public Users.Users Users => users;
}