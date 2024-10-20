using Moq;
using PaperShop.BackPaper.DataAccess.Models;
using PaperShop.BackPaper.DataAccess.RepoInterfaces;
using PaperShop.BackPaper.Services.DTO.Requests;
using PaperShop.BackPaper.Services.Service;
using Xunit;

namespace PaperShop.Test;

public class PaperServiceTests
{
    private readonly Mock<IPaperRepository> _mockPaperRepository;
    private readonly PaperService _paperService;

    public PaperServiceTests()
    {
        _mockPaperRepository = new Mock<IPaperRepository>();
        _paperService = new PaperService(_mockPaperRepository.Object);
    }

    [Fact]
    public void GetAllPapers_ReturnsAllPapers()
    {
        var papers = new List<Paper>
        {
            new Paper { Id = 1, Name = "A4 Copy Paper", Price = 5.99, Stock = 100, Discontinued = false },
            new Paper { Id = 2, Name = "A3 Art Paper", Price = 10.99, Stock = 50, Discontinued = false }
        };

        _mockPaperRepository.Setup(repo => repo.GetAllPapers()).Returns(papers);

        var result = _paperService.GetAllPapers();

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal("A4 Copy Paper", result[0].Name);
        Assert.Equal("A3 Art Paper", result[1].Name);
    }

    [Fact]
    public void CreatePaper_AddsNewPaper()
    {
        var createPaperDto = new CreatePaperDto
        {
            Name = "New Paper",
            Price = 15.0,
            Stock = 200,
            Discontinued = false
        };

        var newPaper = new Paper { Id = 3, Name = "New Paper", Price = 15.0, Stock = 200, Discontinued = false };
        _mockPaperRepository.Setup(repo => repo.Add(It.IsAny<Paper>())).Callback<Paper>(p => p.Id = 3);

        var result = _paperService.CreatePaper(createPaperDto);

        Assert.NotNull(result);
        Assert.Equal("New Paper", result.Name);
        Assert.Equal(15.0, result.Price);
    }
    
    [Fact]
    public void UpdatePaper_UpdatesExistingPaper()
    {
        var updatePaperDto = new UpdatePaperDto
        {
            Name = "Updated Paper",
            Price = 12.0,
            Stock = 150,
            Discontinued = true
        };

        var existingPaper = new Paper { Id = 1, Name = "Old Paper", Price = 10.0, Stock = 100, Discontinued = false };
        _mockPaperRepository.Setup(repo => repo.GetById(1)).Returns(existingPaper);

        _paperService.UpdatePaper(1, updatePaperDto);

        Assert.Equal("Updated Paper", existingPaper.Name);
        Assert.Equal(12.0, existingPaper.Price);
        Assert.Equal(150, existingPaper.Stock);
        Assert.True(existingPaper.Discontinued);
    }

    [Fact]
    public void DeletePaper_DeletesExistingPaper()
    {
        _mockPaperRepository.Setup(repo => repo.GetById(1)).Returns(new Paper { Id = 1 });

        _paperService.DeletePaper(1);

        _mockPaperRepository.Verify(repo => repo.Delete(1), Times.Once);
    }
}