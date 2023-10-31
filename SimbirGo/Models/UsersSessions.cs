namespace TestApi.Models;

public class UsersSessions
{
    public int UsersSessionsId { get; set; }
    
    public int AccountId { get; set; }
    
    public bool ValidSession  { get; set; }

    public virtual Account? Account { get; set; }
}