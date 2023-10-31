using Microsoft.AspNetCore.Mvc;
using TestApi.Blanks.AdminBlanks;
using TestApi.Blanks.UserBlanks;
using TestApi.Models;
using TestApi.Repositories;

namespace TestApi.Services.Transport;

public class TransportServices : ITransportServices
{
    private readonly ITransportRepository _transportRepository;
    private readonly ITransportModelRepository _transportModelRepository;
    private readonly ITransportTypeRepository _transportTypeRepository;
    private readonly IColorRepository _colorRepository;
    private readonly IPriceTypeRepository _priceTypeRepository;

    public TransportServices(ITransportRepository transportRepository,
        ITransportModelRepository transportModelRepository, ITransportTypeRepository transportTypeRepository,
        IColorRepository colorRepository, IPriceTypeRepository priceTypeRepository)
    {
        _transportRepository = transportRepository;
        _transportModelRepository = transportModelRepository;
        _transportTypeRepository = transportTypeRepository;
        _colorRepository = colorRepository;
        _priceTypeRepository = priceTypeRepository;
    }

    public IActionResult GetTransportById(int transportId)
    {
        return new OkObjectResult(_transportRepository.GetTransportById(transportId));
    }

    public IActionResult GetTransports(int start, int count, string type)
    {
        return new OkObjectResult(_transportRepository.GetTransports().Where(t => t.TransportModel.Type.Name == type)
            .ToList().GetRange(start, count));
    }

    public IActionResult AddTransport(UserTransportBlank userTransportBlank, int accountId)
    {
        Models.Transport newTransport = new Models.Transport
        {
            OwnerId = accountId,
            CanBeRented = userTransportBlank.CanBeRented,
            TransportModel = CheckTransportModelExist(userTransportBlank),
            Color = CheckColorExist(userTransportBlank),
            Identifier = userTransportBlank.Identifier,
            Description = userTransportBlank.Description,
            Latitude = userTransportBlank.Latitude,
            Longitude = userTransportBlank.Longitude,

            TransportPriceTypes = new List<TransportPriceType>
            {
                new TransportPriceType
                {
                    PriceType = _priceTypeRepository.GetPriceTypeById(2),
                    Price = userTransportBlank.MinutePrice
                },
                new TransportPriceType
                {
                    PriceType = _priceTypeRepository.GetPriceTypeById(1), Price = userTransportBlank.DayPrice
                }
            }
        };

        _transportRepository.InsertTransport(newTransport);

        return new OkResult();
    }
    public IActionResult AddTransport(AdminTransportBlank adminTransportBlank)
    {
        Models.Transport newTransport = new Models.Transport
        {
            OwnerId = (int)adminTransportBlank.OwnerId,
            CanBeRented = adminTransportBlank.CanBeRented,
            TransportModel = CheckTransportModelExist(adminTransportBlank),
            Color = CheckColorExist(adminTransportBlank),
            Identifier = adminTransportBlank.Identifier,
            Description = adminTransportBlank.Description,
            Latitude = adminTransportBlank.Latitude,
            Longitude = adminTransportBlank.Longitude,

            TransportPriceTypes = new List<TransportPriceType>
            {
                new TransportPriceType
                {
                    PriceType = _priceTypeRepository.GetPriceTypeById(1),
                    Price = adminTransportBlank.MinutePrice
                },
                new TransportPriceType
                {
                    PriceType = _priceTypeRepository.GetPriceTypeById(2), Price = adminTransportBlank.DayPrice
                }
            }
        };

        _transportRepository.InsertTransport(newTransport);

        return new OkResult();
    }

    public IActionResult UpdateTransport(int transportId, UserTransportBlank userTransportBlank, int accountId)
    {
        bool usernameIsOwner = _transportRepository.GetTransportById(transportId).Owner.AccountId == accountId ? true : false;

        if (!usernameIsOwner)
            return new BadRequestObjectResult("The user is not the owner of a transport");
        
        Models.Transport editableTransport = _transportRepository.GetTransportById(transportId)!;

        editableTransport.OwnerId = accountId;
        editableTransport.CanBeRented = userTransportBlank.CanBeRented;
        editableTransport.TransportModel = CheckTransportModelExist(userTransportBlank);
        editableTransport.Color = CheckColorExist(userTransportBlank);
        editableTransport.Identifier = userTransportBlank.Identifier;
        editableTransport.Description = userTransportBlank.Description;
        editableTransport.Latitude = userTransportBlank.Latitude;
        editableTransport.Longitude = userTransportBlank.Longitude;

        editableTransport.TransportPriceTypes[0].Price = userTransportBlank.MinutePrice;
        editableTransport.TransportPriceTypes[1].Price = userTransportBlank.DayPrice;
        
        _transportRepository.UpdateTransport(editableTransport);

        return new OkResult();
    }
    public IActionResult UpdateTransport(int transportId, AdminTransportBlank adminTransportBlank)
    {
        
        Models.Transport editableTransport = _transportRepository.GetTransportById(transportId)!;

        editableTransport.OwnerId = (int)adminTransportBlank.OwnerId;
        editableTransport.CanBeRented = adminTransportBlank.CanBeRented;
        editableTransport.TransportModel = CheckTransportModelExist(adminTransportBlank);
        editableTransport.Color = CheckColorExist(adminTransportBlank);
        editableTransport.Identifier = adminTransportBlank.Identifier;
        editableTransport.Description = adminTransportBlank.Description;
        editableTransport.Latitude = adminTransportBlank.Latitude;
        editableTransport.Longitude = adminTransportBlank.Longitude;

        editableTransport.TransportPriceTypes[0].Price = adminTransportBlank.MinutePrice;
        editableTransport.TransportPriceTypes[1].Price = adminTransportBlank.DayPrice;
        
        _transportRepository.UpdateTransport(editableTransport);

        return new OkResult();
    }

    public IActionResult DeleteTransport(int transportId, int accountId)
    {
        bool usernameIsOwner= _transportRepository.GetTransportById(transportId).Owner.AccountId == accountId ? true : false;

        if (!usernameIsOwner)
            return new BadRequestObjectResult("The user is not the owner of a transport");
        
        _transportRepository.DeleteTransport(transportId);
        return new OkResult();
    }
    public IActionResult DeleteTransport(int transportId)
    {
        _transportRepository.DeleteTransport(transportId);
        return new OkResult();
    }

    #region CheckExist
    
    private TransportModel CheckTransportModelExist(UserTransportBlank userTransportBlank)
    {
        TransportModel? transportModel = _transportModelRepository.GetTransportModelByName(userTransportBlank.Model) ?? new TransportModel
        {
            Type = _transportTypeRepository.GetTransportTypeByName(userTransportBlank.TransportType)!,
            Name = userTransportBlank.Model
        };
        
        return transportModel;
    }
    private TransportModel CheckTransportModelExist(AdminTransportBlank adminTransportBlank)
    {
        TransportModel? transportModel = _transportModelRepository.GetTransportModelByName(adminTransportBlank.Model) ?? new TransportModel
        {
            Type = _transportTypeRepository.GetTransportTypeByName(adminTransportBlank.TransportType)!,
            Name = adminTransportBlank.Model
        };
        
        return transportModel;
    }

    private Color CheckColorExist(UserTransportBlank userTransportBlank)
    {
        Color color = _colorRepository.GetColorByName(userTransportBlank.Color) ?? new Color
        {
            Name = userTransportBlank.Color
        };

        return color;
    }
    private Color CheckColorExist(AdminTransportBlank adminTransportBlank)
    {
        Color color = _colorRepository.GetColorByName(adminTransportBlank.Color) ?? new Color
        {
            Name = adminTransportBlank.Color
        };

        return color;
    }

    #endregion
}