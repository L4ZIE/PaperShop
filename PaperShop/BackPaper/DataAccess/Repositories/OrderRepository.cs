using Microsoft.EntityFrameworkCore;
using PaperShop.BackPaper.DataAccess.Models;


namespace PaperShop.BackPaper.DataAccess.Repositories;

public class OrderRepository
{
    private readonly PaperShopContext _context;

    public OrderRepository(PaperShopContext context)
    {
        _context = context;
    }

    public List<Order> GetAllOrders()
    {
        return _context.Orders.ToList();
    }

    public Order? GetById(int id)
    {
        return _context.Orders
            .Include(o => o.OrderEntries)
            .ThenInclude(oe => oe.Product)
            .FirstOrDefault(o => o.Id == id);
    }
    
    public Order Add(Order order)
    {
        _context.Orders.Add(order);
        _context.SaveChanges();
        return order;
    }
    
    public void Update(Order order)
    {
        _context.Orders.Update(order);
        _context.SaveChanges();
    }
    
    public void Delete(int id)
    {
        var order = _context.Orders.Find(id);
        if (order != null)
        {
            _context.Orders.Remove(order);
            _context.SaveChanges();
        }
    }
}