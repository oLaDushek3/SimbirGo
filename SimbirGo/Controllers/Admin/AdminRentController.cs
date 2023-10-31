using Microsoft.AspNetCore.Mvc;
using TestApi.Blanks.AdminBlanks;
using TestApi.Services.Rent;

namespace TestApi.Controllers.Admin;

[Jwt.Authorize(true)]
[ApiController]
public class AdminRentController : ControllerBase
{
    private readonly IRentServices _rentServices;
    
    public AdminRentController(IRentServices rentServices)
    {
        _rentServices = rentServices;
    }
    
    [HttpGet]
    [Route("api/Admin/Rent/{rentId}")]
    public IActionResult GetRentById(int rentId)
    {
        return _rentServices.GetRentById(rentId);
    }
    
    [HttpGet]
    [Route("api/Admin/UserHistory/{userId}")]
    public IActionResult GetRentAccountHistory(int userId)
    {
        return _rentServices.GetRentByAccountId(userId);
    }
    
    [HttpGet]
    [Route("api/Admin/TransportHistory/{transportId}")]
    public IActionResult GetRentTransportHistory(int transportId)
    {
        return _rentServices.GetRentByTransportId(transportId);
    }
    
    [HttpPost]
    [Route("api/Admin/Rent")]
    public IActionResult AddNewRent(AdminRentBlank adminRentBlank)
    {
        return _rentServices.AddRent(adminRentBlank);
    }
    
    [HttpPost]
    [Route("api/Admin/Rent/End/{rentId}")]
    public IActionResult UpdateRent(int rentId, double latitude, double longitude)
    {
        return _rentServices.UpdateRent(rentId, latitude, longitude);
    }
    
    [HttpPut]
    [Route("api/Admin/Rent/End{id}")]
    public IActionResult UpdateRent(AdminRentBlank adminRentBlank, int id)
    {
        return _rentServices.UpdateRent(adminRentBlank, id);
    }
    
    [HttpPost]
    [Route("api/Admin/Rent/{rentId}")]
    public IActionResult DeleteRent(int rentId)
    {
        return _rentServices.DeleteRent(rentId);
    }
}