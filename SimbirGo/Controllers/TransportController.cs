using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestApi.Blanks.UserBlanks;
using TestApi.Services.Transport;

namespace TestApi.Controllers;

[Jwt.Authorize(false)]
[ApiController]
public class TransportController : ControllerBase
{
    private readonly ITransportServices _transportServices;
    private int LoginUserId => int.Parse(User.Claims.Where(c => c.Type == "id").First().Value);
    
    public TransportController(ITransportServices transportServices)
    {
        _transportServices = transportServices;
    }
    
    [AllowAnonymous]
    [HttpGet]
    [Route("api/[controller]/{id}")]
    public IActionResult GetTransportById(int id)
    {
        return _transportServices.GetTransportById(id);
    }
    
    [HttpPost]
    [Route("api/[controller]/")]
    public IActionResult PostTransport(UserTransportBlank userTransportBlank)
    {
        return _transportServices.AddTransport(userTransportBlank, LoginUserId);
    }
    
    [HttpPut]
    [Route("api/[controller]/{id}")]
    public IActionResult PutTransport(int id, UserTransportBlank userTransportBlank)
    {
        return _transportServices.UpdateTransport(id, userTransportBlank, LoginUserId);
    }
    
    [HttpDelete]
    [Route("api/[controller]/{id}")]
    public IActionResult DeleteTransport(int id)
    {
        return _transportServices.DeleteTransport(id, LoginUserId);
    }
}