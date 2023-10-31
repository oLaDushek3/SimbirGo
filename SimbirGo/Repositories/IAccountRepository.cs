using TestApi.Models;

namespace TestApi.Repositories;

public interface IAccountRepository
{
    IEnumerable<Account> GetAccounts();
    Account? GetAccountById(int accountId);
    Account? GetAccountByUsername(string accountUsername);
    void InsertAccount(Account account);
    void DeleteAccount(int accountId);
    void UpdateAccount(Account account);
}