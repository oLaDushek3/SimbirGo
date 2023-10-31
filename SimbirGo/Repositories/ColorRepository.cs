using Microsoft.EntityFrameworkCore;
using TestApi.Models;

namespace TestApi.Repositories;

public class ColorRepository : IColorRepository
{
    private readonly SimbirGoContext _context;

    public ColorRepository(SimbirGoContext context)
    {
        _context = context;
    }
    
    public IEnumerable<Color> GetColors()
    {
        return _context.Colors.ToList();
    }

    public Color? GetColorById(int colorId)
    {
        return _context.Colors.FirstOrDefault();
    }
    
    public Color? GetColorByName(string colorName)
    {
        return _context.Colors.FirstOrDefault();
    }

    public void InsertColor(Color color)
    {
        _context.Colors.Add(color);
        _context.SaveChanges();
    }

    public void DeleteColor(int colorId)
    {
        _context.Colors.Remove(_context.Colors.First(a => a.ColorId == colorId));
        _context.SaveChanges();
    }

    public void UpdateColor(Color color)
    {        
        _context.Entry(color).State = EntityState.Modified;
        _context.SaveChanges();
    }
}