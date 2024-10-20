using Microsoft.EntityFrameworkCore;
using PaperShop.BackPaper.DataAccess.Models;
using PaperShop.BackPaper.DataAccess.RepoInterfaces;

namespace PaperShop.BackPaper.DataAccess.Repositories;

public class PaperRepository : IPaperRepository
{
    private readonly PaperShopContext _context;

    public PaperRepository(PaperShopContext context)
    {
        _context = context;
    }

    public virtual List<Paper> GetAllPapers()
    {
        return _context.Papers.Include(p => p.PaperProperties).ToList();
    }
    
    public Paper? GetById(int id)
    {
        return _context.Papers
            .Include(p => p.PaperProperties)
            .FirstOrDefault(p => p.Id == id);
    }
    
    public Paper Add(Paper paper)
    {
        _context.Papers.Add(paper);
        _context.SaveChanges();
        return paper;
    }
    
    public void Update(Paper paper)
    {
        _context.Papers.Update(paper);
        _context.SaveChanges();
    }
    
    public void Delete(int id)
    {
        var paper = _context.Papers.Find(id);
        if (paper != null)
        {
            _context.Papers.Remove(paper);
            _context.SaveChanges();
        }
    }
}