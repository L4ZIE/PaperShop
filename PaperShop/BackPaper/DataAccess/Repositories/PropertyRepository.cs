using PaperShop.BackPaper.DataAccess.Models;
using PaperShop.BackPaper.DataAccess.RepoInterfaces;


namespace PaperShop.BackPaper.DataAccess.Repositories;

public class PropertyRepository : IPropertyRepository
{
    private readonly PaperShopContext _context;

    public PropertyRepository(PaperShopContext context)
    {
        _context = context;
    }

    public List<Property> GetAllProperties()
    {
        return _context.Properties.ToList();
    }

    public Property? GetById(int id)
    {
        return _context.Properties.Find(id);
    }
    
    public Property Add(Property property)
    {
        _context.Properties.Add(property);
        _context.SaveChanges();
        return property;
    }

    public void Update(Property property)
    {
        _context.Properties.Update(property);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var property = _context.Properties.Find(id);
        if (property != null)
        {
            _context.Properties.Remove(property);
            _context.SaveChanges();
        }

    }
}