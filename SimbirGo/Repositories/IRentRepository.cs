using TestApi.Models;

namespace TestApi.Repositories;

public interface IRentRepository
{
    IEnumerable<Rent> GetRents();
    Rent? GetRentById(int rentId);
    IEnumerable<Rent> GetRentByAccountId(int accountId);
    IEnumerable<Rent> GetRentByTransportId(int transportId);
    void InsertRent(Rent rent);
    void DeleteRent(int rentId);
    void UpdateRent(Rent rent);
}