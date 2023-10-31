using Microsoft.EntityFrameworkCore;
using TestApi.Models;

namespace TestApi.Repositories;

public class TransportRepository : ITransportRepository
{
    private readonly SimbirGoContext _context;

    public TransportRepository(SimbirGoContext context)
    {
        _context = context;
    }
    
    public IEnumerable<Transport> GetTransports()
    {
        return _context.Transports.
            Include(t => t.Color).
            Include(t => t.TransportModel).
                ThenInclude(tm => tm.Type).
            Include(t => t.TransportPriceTypes).
                ThenInclude(tp => tp.PriceType);
    }

    public Transport? GetTransportById(int transportId)
    {
        return _context.Transports.
            Include(t => t.Color).
            Include(t => t.TransportModel).
                ThenInclude(tm => tm.Type).
            Include(t => t.TransportPriceTypes).
                ThenInclude(tp => tp.PriceType).
            FirstOrDefault(a => a.TransportId == transportId);
    }

    public void InsertTransport(Transport transport)
    {
        _context.Transports.Add(transport);
        _context.SaveChanges();
    }

    public void DeleteTransport(int transportId)
    {
        _context.Transports.Remove(_context.Transports.First(a => a.TransportId == transportId));
        _context.SaveChanges();
    }

    public void UpdateTransport(Transport transport)
    {
        _context.Entry(transport).State = EntityState.Modified;
        _context.SaveChanges();
    }
}