using Microsoft.AspNetCore.Mvc;
using PaperShop.BackPaper.Services.DTO.Requests;
using PaperShop.BackPaper.Services.DTO.Responses;
using PaperShop.BackPaper.Services.Service;

namespace PaperShop.BackPaper.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly OrderService _orderService;

    public OrderController(OrderService orderService)
    {
        _orderService = orderService;
    }
    
    [HttpGet]
    public ActionResult<List<OrderDto>> GetAllOrders()
    {
        var orders = _orderService.GetAllOrders();
        return Ok(orders);
    }
    
    [HttpGet("{id}")]
    public ActionResult<OrderDto> GetOrderById(int id)
    {
        var order = _orderService.GetOrderById(id);
        if (order == null)
        {
            return NotFound(new { Message = $"Order with ID {id} not found." });
        }
        return Ok(order);
    }
    
    [HttpPost]
    public ActionResult<OrderDto> CreateOrder(CreateOrderDto createOrderDto)
    {
        var createdOrder = _orderService.CreateOrder(createOrderDto);
        return CreatedAtAction(nameof(GetOrderById), new { id = createdOrder.Id }, createdOrder);
    }
    
    [HttpPut("{id}")]
    public IActionResult UpdateOrder(int id, UpdateOrderDto updateOrderDto)
    {
        _orderService.UpdateOrder(id, updateOrderDto);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public IActionResult DeleteOrder(int id)
    {
        _orderService.DeleteOrder(id);
        return NoContent();
    }
}