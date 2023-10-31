using Microsoft.EntityFrameworkCore;
using TestApi.Models;

namespace TestApi.Repositories;

public class PriceTypeRepository : IPriceTypeRepository
{
    private readonly SimbirGoContext _context;

    public PriceTypeRepository(SimbirGoContext context)
    {
        _context = context;
    }
    
    public IEnumerable<PriceType> GetPriceTypes()
    {
        return _context.PriceTypes.ToList();
    }

    public PriceType? GetPriceTypeById(int priceTypeId)
    {
        return _context.PriceTypes.FirstOrDefault();
    }
    
    public PriceType? GetPriceTypeByName(string priceTypeName)
    {
        return _context.PriceTypes.FirstOrDefault();
    }

    public void InsertPriceType(PriceType priceType)
    {
        _context.PriceTypes.Add(priceType);
        _context.SaveChanges();
    }

    public void DeletePriceType(int priceTypeId)
    {
        _context.PriceTypes.Remove(_context.PriceTypes.First(a => a.PriceTypeId == priceTypeId));
        _context.SaveChanges();
    }

    public void UpdatePriceType(PriceType priceType)
    {        
        _context.Entry(priceType).State = EntityState.Modified;
        _context.SaveChanges();
    }
}