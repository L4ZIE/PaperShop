using Microsoft.AspNetCore.Mvc;
using PaperShop.BackPaper.DataAccess;
using PaperShop.BackPaper.DataAccess.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly PaperShopContext _context;

    public CustomerController(PaperShopContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetCustomers()
    {
        var customers = await _context.Customers
            .Select(c => new { c.Id, c.Name })
            .ToListAsync();

        return Ok(customers);
    }
}