using StockManagementSystem.DTOs;
using StockManagementSystem.Models;
namespace StockManagementSystem.Repositories
{
    public interface IOrderRepository
    {
        Task<mResult<IEnumerable<Order>>> GetAllOrdersAsync(string? product, string? customer, string? salesPerson, DateTime? fromDate, DateTime? toDate);
        Task<mResult<IEnumerable<SalesRanking>>> GetAllOrdersSalesRankingAsync(string? product, string? customer, string? salesPerson, DateTime? fromDate, DateTime? toDate);
        Task<mResult<Order?>> GetOrderByIdAsync(int id);
        Task<mResult<bool>> AddOrderAsync(Order order);
        Task<mResult<bool>> UpdateOrderAsync(Order order);
        Task<mResult<bool>> DeleteOrderAsync(int id);
        Task<mResult<IEnumerable<mCodeDesc>>> GetSalesPersonsAsync();
        Task<mResult<IEnumerable<mCodeDesc>>> GetCustomersAsync();
    }
}
