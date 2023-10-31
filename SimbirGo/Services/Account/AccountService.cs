using Microsoft.AspNetCore.Mvc;
using TestApi.Blanks.AdminBlanks;
using TestApi.Blanks.UserBlanks;
using TestApi.Jwt;
using TestApi.Models;
using TestApi.Repositories;

namespace TestApi.Services.Account;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly GenerateJwtToken _generateJwtToken;
    private readonly IUsersSessionsRepository _usersSessionsRepository;
    
    public AccountService(IAccountRepository accountRepository, JwtOptions jwtOptions, IUsersSessionsRepository usersSessionsRepository)
    {
        _accountRepository = accountRepository;
        _usersSessionsRepository = usersSessionsRepository;
        _generateJwtToken = new GenerateJwtToken(jwtOptions, usersSessionsRepository);
    }

    public IActionResult GetAccountById(int accountId)
    {
        return new OkObjectResult(_accountRepository.GetAccountById(accountId));
    }
    
    public IActionResult GetAccounts(int start, int count)
    {
        return new OkObjectResult(_accountRepository.GetAccounts().ToList().GetRange(start, count));
    }

    public IActionResult SignIn(UserAccountBlank userAccountBlank)
    {
        var user = _accountRepository.GetAccountByUsername(userAccountBlank.Username);

        if (user == null)
            return new UnauthorizedResult();

        if (user.Password != Md5.HashPassword(userAccountBlank.Password))
            return new UnauthorizedResult();
        
        var tokenString = _generateJwtToken.GetJwtToken(user);
        return new OkObjectResult(new { Token = tokenString, Message = "Success" });
    }

    public IActionResult InsertAccount(UserAccountBlank userAccountBlank)
    {
        bool usernameIsUnique = _accountRepository.GetAccountByUsername(userAccountBlank.Username) == null ? true : false;

        if (!usernameIsUnique)
            return new BadRequestObjectResult("Username is not unique");
        
        var newAccount = new Models.Account
        {
            Username = userAccountBlank.Username,
            Password = Md5.HashPassword(userAccountBlank.Password),
            IsAdmin = false,
            Balance = 0
        };

        _accountRepository.InsertAccount(newAccount);
        
        return new OkResult();
    }
    public IActionResult InsertAccount(AdminAccountBlank adminAccountBlank)
    {
        bool usernameIsUnique = _accountRepository.GetAccountByUsername(adminAccountBlank.Username) == null ? true : false;

        if (!usernameIsUnique)
            return new BadRequestObjectResult("Username is not unique");
        
        var newAccount = new Models.Account
        {
            Username = adminAccountBlank.Username,
            Password = Md5.HashPassword(adminAccountBlank.Password),
            IsAdmin = adminAccountBlank.IsAdmin,
            Balance = adminAccountBlank.Balance
        };

        _accountRepository.InsertAccount(newAccount);
        
        return new OkResult();
    }

    public IActionResult SignOut(int accountId)
    {
        UsersSessions editableUsersSessions = _usersSessionsRepository.GetUsersSessionsByAccountId(accountId);
        editableUsersSessions.ValidSession = false;
        _usersSessionsRepository.UpdateUsersSessions(editableUsersSessions);

        return new OkResult();
    }

    public IActionResult DeleteAccount(int accountId)
    {
        _accountRepository.DeleteAccount(accountId);
        return new OkResult();
    }
    
    public IActionResult UpdateAccount(UserAccountBlank userAccountBlank, int accountId)
    {
        bool usernameIsUnique = _accountRepository.GetAccountByUsername(userAccountBlank.Username) == null ? true : false;

        if (!usernameIsUnique)
            return new BadRequestObjectResult("Username is not unique");
        
        Models.Account editableAccount = _accountRepository.GetAccountById(accountId)!;
        
        editableAccount.Username = userAccountBlank.Username;
        editableAccount.Password = Md5.HashPassword(userAccountBlank.Username);
        
        _accountRepository.UpdateAccount(editableAccount);

        return new OkResult();
    }
    public IActionResult UpdateAccount(AdminAccountBlank adminAccountBlank, int accountId)
    {
        bool usernameIsUnique = _accountRepository.GetAccountByUsername(adminAccountBlank.Username) == null ? true : false;

        if (!usernameIsUnique)
            return new BadRequestObjectResult("Username is not unique");
        
        Models.Account editableAccount = _accountRepository.GetAccountById(accountId)!;
        
        editableAccount.Username = adminAccountBlank.Username;
        editableAccount.Password = Md5.HashPassword(adminAccountBlank.Username);
        editableAccount.IsAdmin = adminAccountBlank.IsAdmin;
        editableAccount.Balance = adminAccountBlank.Balance;
        
        _accountRepository.UpdateAccount(editableAccount);

        return new OkResult();
    }
    
    public IActionResult Hesoyam(int accountId, bool isAdmin, int lofinAccountId)
    {
        Models.Account account =_accountRepository.GetAccountById(accountId);

        if (account.AccountId != lofinAccountId && !isAdmin)
            return new BadRequestObjectResult("No rights");
        
        account.Balance += 250000;
        
        _accountRepository.UpdateAccount(account);
            
        return new OkResult();
    }
}