using Microsoft.EntityFrameworkCore;
using TestApi.Models;

namespace TestApi.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly SimbirGoContext _context;

    public AccountRepository(SimbirGoContext context)
    {
        _context = context;
    }
    
    public IEnumerable<Account> GetAccounts()
    {
        return _context.Accounts.ToList();
    }

    public Account? GetAccountById(int accountId)
    {
        return _context.Accounts.First(a => a.AccountId == accountId);
    }
    
    public Account? GetAccountByUsername(string accountUsername)
    {
        return _context.Accounts.FirstOrDefault(a => a.Username == accountUsername);
    }

    public void InsertAccount(Account account)
    {
        _context.Accounts.Add(account);
        _context.SaveChanges();
    }

    public void DeleteAccount(int accountId)
    {
        _context.Accounts.Remove(_context.Accounts.First(a => a.AccountId == accountId));
        _context.SaveChanges();
    }

    public void UpdateAccount(Account account)
    {
        _context.Entry(account).State = EntityState.Modified;
        _context.SaveChanges();
    }
}