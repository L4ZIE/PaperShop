using PaperShop.BackPaper.DataAccess.RepoInterfaces;
using PaperShop.BackPaper.DataAccess.Repositories;
using PaperShop.BackPaper.Services.DTO.Requests;
using PaperShop.BackPaper.Services.DTO.Responses;

namespace PaperShop.BackPaper.Services.Service;

public class PaperService
{
    private readonly IPaperRepository _paperRepository;

    public PaperService(IPaperRepository paperRepository)
    {
        _paperRepository = paperRepository;
    }

    public List<PaperDto> GetAllPapers()
    {
        var papers = _paperRepository.GetAllPapers();
        return papers.Select(p => PaperDto.FromEntity(p)).ToList();
    }
    
    public PaperDto GetPaperById(int id)
    {
        var paper = _paperRepository.GetById(id);
        return paper != null ? PaperDto.FromEntity(paper) : null;
    }
    
    public PaperDto CreatePaper(CreatePaperDto createPaperDto)
    {
        var paper = createPaperDto.ToEntity();
        _paperRepository.Add(paper);
        return PaperDto.FromEntity(paper);
    }

    public void UpdatePaper(int id, UpdatePaperDto updatePaperDto)
    {
        var paper = _paperRepository.GetById(id);
        if (paper == null) return;

        paper.Name = updatePaperDto.Name ?? paper.Name;
        paper.Price = updatePaperDto.Price ?? paper.Price;
        paper.Stock = updatePaperDto.Stock ?? paper.Stock;
        paper.Discontinued = updatePaperDto.Discontinued ?? paper.Discontinued;

        _paperRepository.Update(paper);
    }

    public void DeletePaper(int id)
    {
        _paperRepository.Delete(id);
    }
}