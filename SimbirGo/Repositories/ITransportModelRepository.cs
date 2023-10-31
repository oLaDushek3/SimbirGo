using TestApi.Models;

namespace TestApi.Repositories;

public interface ITransportModelRepository
{
    IEnumerable<TransportModel> GetTransportModels();
    TransportModel? GetTransportModelById(int transportModelId);
    TransportModel? GetTransportModelByName(string transportModelName);
    void InsertTransportModel(TransportModel transportModel);
    void DeleteTransportModel(int transportModelId);
    void UpdateTransportModel(TransportModel transportModel);
}