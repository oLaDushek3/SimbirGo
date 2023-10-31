using TestApi.Models;

namespace TestApi.Repositories;

public interface ITransportTypeRepository
{
    IEnumerable<TransportType> GetTransportTypes();
    TransportType? GetTransportTypeById(int transportTypeId);
    TransportType? GetTransportTypeByName(string transportTypeName);
    void InsertTransportType(TransportType transportType);
    void DeleteTransportType(int transportTypeId);
    void UpdateTransportType(TransportType transportType);
}