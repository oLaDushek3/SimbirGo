using Microsoft.AspNetCore.Mvc;
using TestApi.Services.Account;

namespace TestApi.Controllers;

[Jwt.Authorize(false)]
[ApiController]
public class PaymentController : ControllerBase
{    
    private readonly IAccountService _accountService;
    
    private bool IsAdmin => bool.Parse(User.Claims.Where(c => c.Type == "isAdmin").First().Value);
    private int LoginUserId => int.Parse(User.Claims.Where(c => c.Type == "id").First().Value);
    
    public PaymentController(IAccountService accountService)
    {
        _accountService = accountService;
    }
    
    [HttpPost]
    [Route("api/[controller]/Hesoyam/{accountId}")]
    public IActionResult Hesoyam(int accountId)
    {
        return _accountService.Hesoyam(accountId, IsAdmin, LoginUserId);
    }
}