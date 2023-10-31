using TestApi.Models;

namespace TestApi.Repositories;

public interface ITransportRepository
{
    IEnumerable<Transport> GetTransports();
    Transport? GetTransportById(int transportId);
    void InsertTransport(Transport transport);
    void DeleteTransport(int transportId);
    void UpdateTransport(Transport transport);
}