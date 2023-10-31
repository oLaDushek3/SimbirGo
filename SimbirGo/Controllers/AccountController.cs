using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestApi.Blanks.UserBlanks;
using TestApi.Services.Account;

namespace TestApi.Controllers;

[Jwt.Authorize(false)]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    
    private int LoginUserId => int.Parse(User.Claims.Where(c => c.Type == "id").First().Value);
    
    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }
    
    [HttpGet]
    [Route("api/[controller]/Me")]
    public IActionResult GetLoginAccountInfo()
    {
        return _accountService.GetAccountById(LoginUserId);
    }
    
    [AllowAnonymous]
    [HttpPost]
    [Route("api/[controller]/SignIn")]
    public IActionResult SignIn(UserAccountBlank userAccountBlank)
    {
        return _accountService.SignIn(userAccountBlank);
    }
    
    [AllowAnonymous]
    [HttpPost]
    [Route("api/[controller]/SignUp")]
    public IActionResult SignUp(UserAccountBlank userAccountBlank)
    {
        return _accountService.InsertAccount(userAccountBlank);
    }
    
    [HttpPost]
    [Route("api/[controller]/SignOut")]
    public IActionResult SignOut()
    {
        return _accountService.SignOut(LoginUserId);
    }
    
    [HttpPut]
    [Route("api/[controller]/Update")]
    public IActionResult UpdateAccount(UserAccountBlank userAccountBlank)
    {
        return _accountService.UpdateAccount(userAccountBlank, LoginUserId);
    }
}