using Microsoft.AspNetCore.Mvc;
using TestApi.Blanks.AdminBlanks;
using TestApi.Models;
using TestApi.Repositories;

namespace TestApi.Services.Rent;

public class RentServices : IRentServices
{
    private readonly ITransportRepository _transportRepository;
    private readonly IRentRepository _rentRepository;
    private readonly IAccountRepository _accountRepository;

    public RentServices(ITransportRepository transportRepository,IRentRepository rentRepository, IAccountRepository accountRepository)
    {
        _transportRepository = transportRepository;
        _rentRepository = rentRepository;
        _accountRepository = accountRepository;
    }
    
    public IActionResult GetRentById(int rentId, int accountId)
    {
        Models.Rent selectedRent = _rentRepository.GetRentById(rentId);
        
        if(selectedRent.TransportPriceType.TransportId != accountId || selectedRent.AccountId != accountId)
            return new BadRequestObjectResult("No rights");

        return new OkObjectResult(selectedRent);
    }  
    public IActionResult GetRentById(int rentId)
    {
        return new OkObjectResult(_rentRepository.GetRentById(rentId));
    }

    public IActionResult GetCanBeRentedTransport(double latitude, double longitude, double radius, string type)
    {
        List<Models.Transport> allTransport = _transportRepository.GetTransports().ToList();
        List<Models.Transport> canBeRentedTransport = new List<Models.Transport>();

        foreach (var transport in allTransport)
        {
            if(Math.Sqrt(Math.Pow(transport.Latitude - latitude, 2) + Math.Pow(transport.Longitude - longitude, 2)) <= radius &&
               transport.TransportModel.Type.Name == type)
                canBeRentedTransport.Add(transport);
        }

        return new OkObjectResult(canBeRentedTransport);
    }

    public IActionResult GetRentByTransportId(int transportId, int accountId)
    {
        Models.Transport transport = _transportRepository.GetTransportById(transportId);
        if(transport == null)
            return new BadRequestObjectResult("Wrong id");
        
        if (transport.OwnerId != accountId)
            return new BadRequestObjectResult("No rights");
            
        return new OkObjectResult(_rentRepository.GetRentByTransportId(transportId));
    }
    public IActionResult GetRentByTransportId(int transportId)
    {
        Models.Transport transport = _transportRepository.GetTransportById(transportId);
        if(transport == null)
            return new BadRequestObjectResult("Wrong id");
            
        return new OkObjectResult(_rentRepository.GetRentByTransportId(transportId));
    }
    
    public IActionResult GetRentByAccountId(int accountId)
    {
        return new OkObjectResult(_rentRepository.GetRentByAccountId(accountId));
    }

    public IActionResult AddRent(int transportId, string rentType, int accountId)
    {
        Models.Transport selectedTransport = _transportRepository.GetTransportById(transportId);
        
        if(selectedTransport == null)
            return new BadRequestObjectResult("Wrong id");
        
        if (selectedTransport.OwnerId == accountId)
            return new BadRequestObjectResult("User is the owner of a transport");
        
        if (!selectedTransport.CanBeRented)
            return new BadRequestObjectResult("Transport busy");
        
        TransportPriceType selectedTransportPriceType = _transportRepository.GetTransportById(transportId)
            .TransportPriceTypes.First(tpt => tpt.PriceType.Name == rentType);
        Models.Rent newRent = new Models.Rent
        {
            AccountId = accountId,
            TransportPriceType = selectedTransportPriceType,
            PriceOfUnit = (double)selectedTransportPriceType.Price,
            TimeStart = DateTime.Now,
        };

        _rentRepository.InsertRent(newRent);

        selectedTransport.CanBeRented = false;
        _transportRepository.UpdateTransport(selectedTransport);
        
        return new OkResult();
    }
    public IActionResult AddRent(AdminRentBlank adminRentBlank)
    {
        Models.Transport selectedTransport = _transportRepository.GetTransportById((int)adminRentBlank.TransportId);
        
        if(selectedTransport == null)
            return new BadRequestObjectResult("Wrong id");
        
        TransportPriceType selectedTransportPriceType = selectedTransport.TransportPriceTypes.First(tpt => tpt.PriceType.Name == adminRentBlank.PriceType);
        
        Models.Rent newRent = new Models.Rent
        {
            AccountId = (int)adminRentBlank.UserId,
            TransportPriceType = selectedTransportPriceType,
            PriceOfUnit = selectedTransportPriceType.Price,
            TimeStart = DateTime.Parse(adminRentBlank.TimeStart),
            TimeEnd = DateTime.Parse(adminRentBlank.TimeEnd),
            FinalPrice = adminRentBlank.FinalPrice
        };

        _rentRepository.InsertRent(newRent);

        if(newRent.TimeEnd == null)
            selectedTransport.CanBeRented = false;
        else
            selectedTransport.CanBeRented = true;
        
        _transportRepository.UpdateTransport(selectedTransport);
        
        return new OkResult();
    }

    public IActionResult UpdateRent(int rentId, double latitude, double longitude, int accountId)
    {
        Models.Rent editableRent = _rentRepository.GetRentById(rentId);
        Models.Account renter = _accountRepository.GetAccountById(accountId);
        Models.Transport selectedTransport = _transportRepository.GetTransportById(editableRent.TransportPriceType.TransportId);
        
        if (renter.AccountId != accountId)        
            return new BadRequestObjectResult("No rights");
        
        editableRent.TransportPriceType.Transport.Latitude = latitude;
        editableRent.TransportPriceType.Transport.Longitude = longitude;
        editableRent.TimeEnd = DateTime.Now;
        
        if (editableRent.TransportPriceType.PriceType.PriceTypeId == 1)
            editableRent.FinalPrice = (DateTime.Now - editableRent.TimeStart).TotalMinutes * editableRent.PriceOfUnit;
        else
            editableRent.FinalPrice = (DateTime.Now - editableRent.TimeStart).TotalDays * editableRent.PriceOfUnit;

        _rentRepository.UpdateRent(editableRent);
        
        renter.Balance -= (double)editableRent.FinalPrice;
        
        _accountRepository.UpdateAccount(renter);
        
        selectedTransport.CanBeRented = false;
        _transportRepository.UpdateTransport(selectedTransport);
        
        return new OkResult();
    }
    public IActionResult UpdateRent(int rentId, double latitude, double longitude)
    {
        Models.Rent editableRent = _rentRepository.GetRentById(rentId);
        Models.Account renter = _accountRepository.GetAccountById(editableRent.AccountId);
        Models.Transport selectedTransport = _transportRepository.GetTransportById(editableRent.TransportPriceType.TransportId);
        
        editableRent.TransportPriceType.Transport.Latitude = latitude;
        editableRent.TransportPriceType.Transport.Longitude = longitude;
        editableRent.TimeEnd = DateTime.Now;
        
        if (editableRent.TransportPriceType.PriceType.PriceTypeId == 1)
            editableRent.FinalPrice = (DateTime.Now - editableRent.TimeStart).TotalMinutes * editableRent.PriceOfUnit;
        else
            editableRent.FinalPrice = (DateTime.Now - editableRent.TimeStart).TotalDays * editableRent.PriceOfUnit;

        _rentRepository.UpdateRent(editableRent);
        
        renter.Balance -= (double)editableRent.FinalPrice;
        
        _accountRepository.UpdateAccount(renter);
        
        selectedTransport.CanBeRented = false;
        _transportRepository.UpdateTransport(selectedTransport);
        
        return new OkResult();
    }
    public IActionResult UpdateRent(AdminRentBlank adminRentBlank, int rentId)
    {
        Models.Rent selectedRent = _rentRepository.GetRentById(rentId);
        Models.Transport selectedTransport = _transportRepository.GetTransportById((int)adminRentBlank.TransportId);
        
        if(selectedTransport == null || selectedRent == null)
            return new BadRequestObjectResult("Wrong id");
        
        TransportPriceType selectedTransportPriceType = selectedTransport.TransportPriceTypes.First(tpt => tpt.PriceType.Name == adminRentBlank.PriceType);

        selectedRent.AccountId = (int)adminRentBlank.UserId;
        selectedRent.TransportPriceType = selectedTransportPriceType;
        selectedRent.PriceOfUnit = selectedTransportPriceType.Price;
        selectedRent.TimeStart = DateTime.Parse(adminRentBlank.TimeStart);
        selectedRent.TimeEnd = DateTime.Parse(adminRentBlank.TimeEnd);
        selectedRent.FinalPrice = adminRentBlank.FinalPrice;
        
        _rentRepository.UpdateRent(selectedRent);
        
        if(selectedRent.TimeEnd == null)
            selectedTransport.CanBeRented = false;
        else
            selectedTransport.CanBeRented = true;
        
        _transportRepository.UpdateTransport(selectedTransport);
        
        return new OkResult();
    }

    public IActionResult DeleteRent(int rentId)
    {
        _rentRepository.DeleteRent(rentId);
        return new OkResult();
    }
}