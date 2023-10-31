using TestApi.Models;

namespace TestApi.Repositories;

public interface IPriceTypeRepository
{
    IEnumerable<PriceType> GetPriceTypes();
    PriceType? GetPriceTypeById(int priceTypeId);
    PriceType? GetPriceTypeByName(string priceTypeName);
    void InsertPriceType(PriceType priceType);
    void DeletePriceType(int priceTypeId);
    void UpdatePriceType(PriceType priceType);
}