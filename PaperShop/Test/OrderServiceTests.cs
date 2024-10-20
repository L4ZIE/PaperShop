using Moq;
using PaperShop.BackPaper.DataAccess.Models;
using PaperShop.BackPaper.DataAccess.RepoInterfaces;
using PaperShop.BackPaper.Services.Service;
using Xunit;

namespace PaperShop.Test
{
    public class OrderServiceTests
    {
        private readonly Mock<IOrderRepository> _mockOrderRepository;
        private readonly OrderService _orderService;

        public OrderServiceTests()
        {
            _mockOrderRepository = new Mock<IOrderRepository>();
            _orderService = new OrderService(_mockOrderRepository.Object);
        }

        [Fact]
        public void GetAllOrders_ReturnsOrders()
        {
            var orders = new List<Order>
            {
                new Order { Id = 1, TotalAmount = 100 },
                new Order { Id = 2, TotalAmount = 200 }
            };

            _mockOrderRepository.Setup(repo => repo.GetAllOrders()).Returns(orders);

            var result = _orderService.GetAllOrders();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal(100, result.ElementAt(0).TotalAmount);
            Assert.Equal(200, result.ElementAt(1).TotalAmount);
        }

        
    }
}