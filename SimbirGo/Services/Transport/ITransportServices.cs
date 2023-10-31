using Microsoft.AspNetCore.Mvc;
using TestApi.Blanks;
using TestApi.Blanks.AdminBlanks;
using TestApi.Blanks.UserBlanks;

namespace TestApi.Services.Transport;

public interface ITransportServices
{
    public IActionResult GetTransportById(int transportId);
    
    public IActionResult GetTransports(int start, int count, string type);
    
    public IActionResult AddTransport(UserTransportBlank userTransportBlank, int accountId);
    public IActionResult AddTransport(AdminTransportBlank adminTransportBlank);
    
    public IActionResult UpdateTransport(int transportId, UserTransportBlank userTransportBlank, int accountId);
    public IActionResult UpdateTransport(int transportId, AdminTransportBlank adminTransportBlank);
    
    public IActionResult DeleteTransport(int transportId, int accountId);
    public IActionResult DeleteTransport(int transportId);
}