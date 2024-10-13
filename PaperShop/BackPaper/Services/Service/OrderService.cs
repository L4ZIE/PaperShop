using PaperShop.BackPaper.DataAccess.Repositories;
using PaperShop.BackPaper.Services.DTO.Requests;
using PaperShop.BackPaper.Services.DTO.Responses;

namespace PaperShop.BackPaper.Services.Service;

public class OrderService
{
    private readonly OrderRepository _orderRepository;

    public OrderService(OrderRepository orderRepository)
    {
        _orderRepository= orderRepository;
    }

    public List<OrderDto> GetAllOrders()
    {
        var orders = _orderRepository.GetAllOrders();
        return orders.Select( o => OrderDto.FromEntity(o)).ToList();
    }
    
    public OrderDto? GetOrderById(int id)
    {
        var order = _orderRepository.GetById(id);
        return order != null ? OrderDto.FromEntity(order) : null;
    }
    
    public OrderDto CreateOrder(CreateOrderDto createOrderDto)
    {
        var order = createOrderDto.ToEntity();
        _orderRepository.Add(order);
        return OrderDto.FromEntity(order);
    }
    
    public void UpdateOrder(int id, UpdateOrderDto updateOrderDto)
    {
        var order = _orderRepository.GetById(id);
        if (order == null) return;

        order.DeliveryDate = updateOrderDto.DeliveryDate;
        order.TotalAmount = updateOrderDto.TotalAmount ?? order.TotalAmount;
        order.CustomerId = updateOrderDto.CustomerId;
        order.Status = updateOrderDto.Status ?? order.Status;

        _orderRepository.Update(order);
    }
    
    public void DeleteOrder(int id)
    {
        _orderRepository.Delete(id);
    }
}