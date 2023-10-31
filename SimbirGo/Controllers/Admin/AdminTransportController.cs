using Microsoft.AspNetCore.Mvc;
using TestApi.Blanks.AdminBlanks;
using TestApi.Services.Transport;

namespace TestApi.Controllers.Admin;

[Jwt.Authorize(true)]
[ApiController]
public class AdminTransportController : ControllerBase
{
    private readonly ITransportServices _transportServices;
    
    public AdminTransportController(ITransportServices transportServices)
    {
        _transportServices = transportServices;
    }
    
    [HttpGet]
    [Route("api/Admin/Transport/")]
    public IActionResult GetTransports(int start, int count, string type)
    {
        return _transportServices.GetTransports(start, count, type);
    }
    
    [HttpGet]
    [Route("api/Admin/Transport/{id}")]
    public IActionResult GetTransportById(int id)
    {
        return _transportServices.GetTransportById(id);
    }
    
    [HttpPost]
    [Route("api/Admin/Transport/")]
    public IActionResult AddNewTransport(AdminTransportBlank adminTransportBlank)
    {
        return _transportServices.AddTransport(adminTransportBlank);
    }
    
    [HttpPut]
    [Route("api/Admin/Transport/{id}")]
    public IActionResult PutTransport(AdminTransportBlank adminTransportBlank, int id)
    {
        return _transportServices.UpdateTransport(id, adminTransportBlank);
    }
    
    [HttpDelete]
    [Route("api/Admin/Transport/{id}")]
    public IActionResult DeleteTransport(int id)
    {
        return _transportServices.DeleteTransport(id);
    }
}