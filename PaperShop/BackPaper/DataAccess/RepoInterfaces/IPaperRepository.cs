using PaperShop.BackPaper.DataAccess.Models;

namespace PaperShop.BackPaper.DataAccess.RepoInterfaces;

public interface IPaperRepository
{
    List<Paper> GetAllPapers();
    Paper? GetById(int id);
    Paper Add(Paper paper);
    void Update(Paper paper);
    void Delete(int id);
}