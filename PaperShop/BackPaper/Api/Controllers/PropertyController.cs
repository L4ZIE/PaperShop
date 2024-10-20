using Microsoft.AspNetCore.Mvc;
using PaperShop.BackPaper.Services.DTO.Requests;
using PaperShop.BackPaper.Services.DTO.Responses;
using PaperShop.BackPaper.Services.Service;

namespace PaperShop.BackPaper.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class PropertyController : ControllerBase
{
    private readonly PropertyService _propertyService;

    public PropertyController(PropertyService propertyService)
    {
        _propertyService = propertyService;
    }
    
    [HttpGet]
    public IActionResult GetAllProperties()
    {
        var properties = _propertyService.GetAllProperties();
        return Ok(properties);
    }

    [HttpGet("{id}")]
    public IActionResult GetPropertyById(int id)
    {
        var property = _propertyService.GetPropertyById(id);
        return property != null ? Ok(property) : NotFound();
    }

    [HttpPost]
    public IActionResult CreateProperty(CreatePropertyDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); 
        }
        
        var createdProperty = _propertyService.CreateProperty(dto);
        return CreatedAtAction(nameof(CreateProperty), new { id = createdProperty.Id }, createdProperty);
    }
    
    [HttpPut("{id}")]
    public IActionResult UpdateProperty(int id, [FromBody] UpdatePropertyDto dto)
    {
        _propertyService.UpdateProperty(dto, id);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProperty(int id)
    {
        _propertyService.DeleteProperty(id);
        return NoContent();
    }
}