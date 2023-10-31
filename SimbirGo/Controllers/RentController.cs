using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestApi.Services.Rent;

namespace TestApi.Controllers;

[Jwt.Authorize(false)]
[ApiController]
public class RentController : ControllerBase
{
    private readonly IRentServices _rentServices;
    
    private int LoginUserId => int.Parse(User.Claims.Where(c => c.Type == "id").First().Value);
    
    public RentController(IRentServices rentServices)
    {
        _rentServices = rentServices;
    }
    
    [AllowAnonymous]
    [HttpGet]
    [Route("api/[controller]/Transport")]
    public IActionResult GetCanBeRentedTransport(double latitude, double longitude, double radius, string type)
    {
        return _rentServices.GetCanBeRentedTransport(latitude, longitude, radius, type);
    }
    
    [HttpGet]
    [Route("api/[controller]/{rentId}")]
    public IActionResult GetRentById(int rentId)
    {
        return _rentServices.GetRentById(rentId, LoginUserId);
    }
    
    [HttpGet]
    [Route("api/[controller]/MyHistory")]
    public IActionResult GetRentAccountHistory()
    {
        return _rentServices.GetRentByAccountId(LoginUserId);
    }
    
    [HttpGet]
    [Route("api/[controller]/TransportHistory/{transportId}")]
    public IActionResult GetRentTransportHistory(int transportId)
    {
        return _rentServices.GetRentByTransportId(transportId, LoginUserId);
    }
    
    [HttpPost]
    [Route("api/[controller]/New/{transportId}")]
    public IActionResult InsertNewRent(int transportId, string rentType)
    {
        return _rentServices.AddRent(transportId, rentType, LoginUserId);
    }
    
    [HttpPost]
    [Route("api/[controller]/End/{rentId}")]
    public IActionResult UpdateRent(int rentId, double latitude, double longitude)
    {
        return _rentServices.UpdateRent(rentId, latitude, longitude ,LoginUserId);
    }
}