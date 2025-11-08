using StockManagementSystem.DTOs;
using StockManagementSystem.Models;
namespace StockManagementSystem.Repositories
{
    public interface IOrderRepository
    {
        Task<mResult<IEnumerable<Order>>> GetAllOrdersAsync();
        Task<mResult<Order?>> GetOrderByIdAsync(int id);
        Task<mResult<bool>> AddOrderAsync(Order order);
        Task<mResult<bool>> UpdateOrderAsync(Order order);
        Task<mResult<bool>> DeleteOrderAsync(int id);
    }
}
