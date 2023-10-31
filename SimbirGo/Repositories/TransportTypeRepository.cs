using Microsoft.EntityFrameworkCore;
using TestApi.Models;

namespace TestApi.Repositories;

public class TransportTypeRepository : ITransportTypeRepository
{    
    private readonly SimbirGoContext _context;

    public TransportTypeRepository(SimbirGoContext context)
    {
        _context = context;
    }
    
    public IEnumerable<TransportType> GetTransportTypes()
    {
        return _context.TransportTypes.ToList();
    }

    public TransportType? GetTransportTypeById(int transportTypeId)
    {
        return _context.TransportTypes.FirstOrDefault(t => t.TransportTypeId == transportTypeId);
    }

    public TransportType? GetTransportTypeByName(string transportTypeName)
    {
        return _context.TransportTypes.FirstOrDefault(t => t.Name == transportTypeName);
    }

    public void InsertTransportType(TransportType transportType)
    {
        _context.TransportTypes.Add(transportType);
        _context.SaveChanges();
    }

    public void DeleteTransportType(int transportTypeId)
    {
        _context.TransportTypes.Remove(_context.TransportTypes.First(a => a.TransportTypeId == transportTypeId));
        _context.SaveChanges();
    }

    public void UpdateTransportType(TransportType transportType)
    {        
        _context.Entry(transportType).State = EntityState.Modified;
        _context.SaveChanges();
    }
}