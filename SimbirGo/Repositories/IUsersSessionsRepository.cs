using TestApi.Models;

namespace TestApi.Repositories;

public interface IUsersSessionsRepository
{
    public UsersSessions? GetUsersSessionsByAccountId(int accountId);

    public void InsertUsersSessions(UsersSessions usersSessions);

    public void UpdateUsersSessions(UsersSessions usersSessions);

}