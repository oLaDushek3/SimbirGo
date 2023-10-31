using Microsoft.AspNetCore.Mvc;
using TestApi.Blanks.AdminBlanks;

namespace TestApi.Services.Rent;

public interface IRentServices
{
    public IActionResult GetRentById(int rentId, int accountId);
    public IActionResult GetRentById(int rentId);
    
    public IActionResult GetCanBeRentedTransport(double latitude, double longitude, double radius, string type);
    
    public IActionResult GetRentByTransportId(int transportId, int accountId);
    public IActionResult GetRentByTransportId(int transportId);
    
    public IActionResult GetRentByAccountId(int accountId);
    
    public IActionResult AddRent(int transportId, string rentType, int accountId);
    public IActionResult AddRent(AdminRentBlank adminRentBlank);
    
    public IActionResult UpdateRent(int rentId, double latitude, double longitude, int accountId);
    public IActionResult UpdateRent(int rentId, double latitude, double longitude);
    public IActionResult UpdateRent(AdminRentBlank adminRentBlank, int rentId);
    
    public IActionResult DeleteRent(int rentId);
}