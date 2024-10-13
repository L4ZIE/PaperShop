using Microsoft.AspNetCore.Mvc;
using PaperShop.BackPaper.Services.DTO.Requests;
using PaperShop.BackPaper.Services.DTO.Responses;
using PaperShop.BackPaper.Services.Service;

namespace PaperShop.BackPaper.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaperController : ControllerBase
{
    private readonly PaperService _paperService;

    public PaperController(PaperService paperService)
    {
        _paperService = paperService;
    }
    
    [HttpGet]
    public ActionResult<List<PaperDto>> GetAllPapers()
    {
        var papers = _paperService.GetAllPapers();
        return Ok(papers);
    }
    
    [HttpGet("{id}")]
    public ActionResult<PaperDto> GetPaperById(int id)
    {
        var paper = _paperService.GetPaperById(id);
        if (paper == null)
        {
            return NotFound(new { Message = $"Paper with ID {id} not found." });
        }
        return Ok(paper);
    }
    
    [HttpPost]
    public ActionResult<PaperDto> CreatePaper(CreatePaperDto createPaperDto)
    {
        var createdPaper = _paperService.CreatePaper(createPaperDto);
        return CreatedAtAction(nameof(GetPaperById), new { id = createdPaper.Id }, createdPaper);
    }
    
    [HttpPut("{id}")]
    public IActionResult UpdatePaper(int id, UpdatePaperDto updatePaperDto)
    {
        _paperService.UpdatePaper(id, updatePaperDto);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public IActionResult DeletePaper(int id)
    {
        _paperService.DeletePaper(id);
        return NoContent();
    }
}