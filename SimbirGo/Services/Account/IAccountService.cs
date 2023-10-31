using Microsoft.AspNetCore.Mvc;
using TestApi.Blanks.AdminBlanks;
using TestApi.Blanks.UserBlanks;

namespace TestApi.Services.Account;

public interface IAccountService
{
    public IActionResult GetAccountById(int accountId);
    
    public IActionResult GetAccounts(int start, int count);
        
    public IActionResult SignIn(UserAccountBlank userAccountBlank);
    
    public IActionResult InsertAccount(UserAccountBlank userAccountBlank);
    public IActionResult InsertAccount(AdminAccountBlank adminAccountBlank);

    public IActionResult SignOut(int accountId);
    
    public IActionResult DeleteAccount(int accountId);
    
    public IActionResult UpdateAccount(UserAccountBlank userAccountBlank, int accountId);
    public IActionResult UpdateAccount(AdminAccountBlank adminAccountBlank, int accountId);

    public IActionResult Hesoyam(int accountId, bool isAdmin, int lofinAccountId);
}