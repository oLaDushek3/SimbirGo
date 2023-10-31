using Microsoft.EntityFrameworkCore;
using TestApi.Models;

namespace TestApi.Repositories;

public class UsersSessionsRepository : IUsersSessionsRepository
{
    private readonly SimbirGoContext _context;

    public UsersSessionsRepository(SimbirGoContext context)
    {
        _context = context;
    }

    public UsersSessions? GetUsersSessionsByAccountId(int accountId)
    {
        return _context.UsersSessions.FirstOrDefault(u => u.AccountId == accountId);
    }
    
    public void InsertUsersSessions(UsersSessions usersSessions)
    {
        _context.UsersSessions.Add(usersSessions);
        _context.SaveChanges();
    }
    
    public void UpdateUsersSessions(UsersSessions usersSessions)
    {
        _context.Entry(usersSessions).State = EntityState.Modified;
        _context.SaveChanges();
    }
}