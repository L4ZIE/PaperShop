using PaperShop.BackPaper.DataAccess.Models;

namespace PaperShop.BackPaper.DataAccess.RepoInterfaces;

public interface IPropertyRepository
{
    List<Property> GetAllProperties();
    Property? GetById(int id);
    Property Add(Property property);
    void Update(Property property);
    void Delete(int id);
}