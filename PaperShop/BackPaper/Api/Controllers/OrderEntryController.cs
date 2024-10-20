using Microsoft.AspNetCore.Mvc;
using PaperShop.BackPaper.DataAccess.Models;
using PaperShop.BackPaper.DataAccess.Repositories;
using PaperShop.BackPaper.Services.DTO.Requests;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PaperShop.BackPaper.DataAccess;
using PaperShop.BackPaper.Services.DTO.Responses;

namespace PaperShop.BackPaper.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderEntryController : ControllerBase
    {
        private readonly PaperShopContext _context;

        public OrderEntryController(PaperShopContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<OrderEntry>> GetOrderEntries()
        {
            var orderEntries = _context.OrderEntries
                .Where(oe => oe.OrderId == null)
                .Select(oe => new OrderEntry
                {
                    Id = oe.Id,
                    Quantity = oe.Quantity,
                    ProductId = oe.ProductId,
                    Product = new Paper
                    {
                        Id = oe.Product.Id,
                        Name = oe.Product.Name,
                        Price = oe.Product.Price,
                        Discontinued = oe.Product.Discontinued,
                        Stock = oe.Product.Stock
                    }
                })
                .ToList();

            return Ok(orderEntries);
        }
        
        [HttpPost]
        public ActionResult AddOrderEntry(UpdateOrderEntryDto request)
        {
            var product = _context.Papers.FirstOrDefault(p => p.Id == request.ProductId);
            if (product == null)
            {
                return NotFound(new { Message = "Product not found" });
            }

            var orderEntry = _context.OrderEntries
                .FirstOrDefault(oe => oe.ProductId == request.ProductId && oe.OrderId == null);

            if (orderEntry == null)
            {
                // Create a new order entry
                orderEntry = new OrderEntry
                {
                    ProductId = request.ProductId,
                    Quantity = request.Quantity
                };
                _context.OrderEntries.Add(orderEntry);
            }
            else
            {
                // Update the existing order entry's quantity
                orderEntry.Quantity += request.Quantity;
                _context.OrderEntries.Update(orderEntry);
            }

            _context.SaveChanges();
            return Ok(new { Message = "Order entry added or updated successfully" });
        }
        
        [HttpPut("{id}")]
        public ActionResult UpdateOrderEntry(int id, UpdateOrderEntryDto request)
        {
            var orderEntry = _context.OrderEntries.FirstOrDefault(oe => oe.Id == id && oe.OrderId == null);
            if (orderEntry == null)
            {
                return NotFound(new { Message = "Order entry not found" });
            }

            // Update the quantity
            orderEntry.Quantity = request.Quantity;
            _context.OrderEntries.Update(orderEntry);
            _context.SaveChanges();

            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public ActionResult DeleteOrderEntry(int id)
        {
            var orderEntry = _context.OrderEntries.FirstOrDefault(oe => oe.Id == id && oe.OrderId == null);
            if (orderEntry == null)
            {
                return NotFound(new { Message = "Order entry not found" });
            }

            _context.OrderEntries.Remove(orderEntry);
            _context.SaveChanges();

            return NoContent();
        }
    }
}

