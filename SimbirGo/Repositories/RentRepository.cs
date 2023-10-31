using Microsoft.EntityFrameworkCore;
using TestApi.Models;

namespace TestApi.Repositories;

public class RentRepository : IRentRepository
{
    private readonly SimbirGoContext _context;

    public RentRepository(SimbirGoContext context)
    {
        _context = context;
    }
    
    public IEnumerable<Rent> GetRents()
    {
        return _context.Rents.
            Include(r => r.TransportPriceType).
                ThenInclude(tpt => tpt.Transport).
            Include(r => r.TransportPriceType).
                ThenInclude(tpt => tpt.PriceType).
            Include(r => r.Account);
    }

    public Rent? GetRentById(int rentId)
    {
        return _context.Rents.
            Include(r => r.TransportPriceType).
                ThenInclude(tpt => tpt.Transport).
            Include(r => r.TransportPriceType).
                ThenInclude(tpt => tpt.PriceType).
            First(r => r.RentId == rentId);
    }
    
    public IEnumerable<Rent> GetRentByAccountId(int accountId)
    {
        return _context.Rents.
            Include(r => r.TransportPriceType).
                ThenInclude(tpt => tpt.Transport).
            Include(r => r.TransportPriceType).
                ThenInclude(tpt => tpt.PriceType).
            Where(r => r.AccountId == accountId);
    }

    public IEnumerable<Rent> GetRentByTransportId(int transportId)
    {
        return _context.Rents.
            Include(r => r.Account).
            Include(r => r.TransportPriceType).
                ThenInclude(tpt => tpt.PriceType).
            Where(r => r.TransportPriceType.TransportId == transportId);
    }

    public void InsertRent(Rent rent)
    {
        _context.Rents.Add(rent);
        _context.SaveChanges();
    }

    public void DeleteRent(int rentId)
    {
        _context.Rents.Remove(_context.Rents.First(a => a.RentId == rentId));
        _context.SaveChanges();
    }

    public void UpdateRent(Rent rent)
    {       
        _context.Entry(rent).State = EntityState.Modified;
        _context.SaveChanges();
    }
}