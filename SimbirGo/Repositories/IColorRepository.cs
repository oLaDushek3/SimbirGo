using TestApi.Models;

namespace TestApi.Repositories;

public interface IColorRepository
{
    IEnumerable<Color> GetColors();
    Color? GetColorById(int colorId);
    Color? GetColorByName(string colorName);
    void InsertColor(Color color);
    void DeleteColor(int colorId);
    void UpdateColor(Color color);
}
