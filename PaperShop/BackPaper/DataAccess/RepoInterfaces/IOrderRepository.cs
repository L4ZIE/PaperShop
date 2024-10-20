using PaperShop.BackPaper.DataAccess.Models;

namespace PaperShop.BackPaper.DataAccess.RepoInterfaces;

public interface IOrderRepository
{
    List<Order> GetAllOrders();
    Order? GetById(int id);
    Order Add(Order order);
    void Update(Order order);
    void Delete(int id);
}