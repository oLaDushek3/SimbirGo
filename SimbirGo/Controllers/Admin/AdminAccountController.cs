using Microsoft.AspNetCore.Mvc;
using TestApi.Blanks.AdminBlanks;
using TestApi.Services.Account;

namespace TestApi.Controllers.Admin;

[Jwt.Authorize(true)]
[ApiController]
public class AdminAccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    
    public AdminAccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet] 
    [Route("api/Admin/Account/")]
    public IActionResult GetAccounts(int start, int count)
    {
        return _accountService.GetAccounts(start, count);
    }
    
    [HttpGet] 
    [Route("api/Admin/Account/{id}")]
    public IActionResult GetAccountById(int id)
    {
        return new OkObjectResult(_accountService.GetAccountById(id));
    }
    
    [HttpPost]
    [Route("api/Admin/Account/")]
    public IActionResult AddNewAccount(AdminAccountBlank adminAccountBlank)
    {
        return _accountService.InsertAccount(adminAccountBlank);
    }
    
    [HttpPut]
    [Route("api/Admin/Account/{id}")]
    public IActionResult UpdateAccount(AdminAccountBlank adminAccountBlank, int id)
    {
        return _accountService.UpdateAccount(adminAccountBlank, id);
    }
    
    [HttpDelete]
    [Route("api/Admin/Account/{id}")]
    public IActionResult DeleteAccount(int id)
    {
        return _accountService.DeleteAccount(id);
    }
}