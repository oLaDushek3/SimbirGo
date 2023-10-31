using Microsoft.EntityFrameworkCore;
using TestApi.Models;

namespace TestApi.Repositories;

public class TransportModelRepository : ITransportModelRepository
{
    private readonly SimbirGoContext _context;

    public TransportModelRepository(SimbirGoContext context)
    {
        _context = context;
    }
    
    public IEnumerable<TransportModel> GetTransportModels()
    {
        return _context.TransportModels.Include(t => t.Type).ToList();
    }

    public TransportModel? GetTransportModelById(int transportModelId)
    {
        return _context.TransportModels.Include(t => t.Type).FirstOrDefault(t => t.TransportModelId == transportModelId);
    }
    
    public TransportModel? GetTransportModelByName(string transportModelName)
    {
        return _context.TransportModels.Include(t => t.Type).FirstOrDefault(t => t.Name == transportModelName);
    }

    public void InsertTransportModel(TransportModel transportModel)
    {
        _context.TransportModels.Add(transportModel);
        _context.SaveChanges();
    }

    public void DeleteTransportModel(int transportModelId)
    {
        throw new NotImplementedException();
    }

    public void UpdateTransportModel(TransportModel transportModel)
    {
        throw new NotImplementedException();
    }
}