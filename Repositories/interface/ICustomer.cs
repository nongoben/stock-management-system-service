using StockManagementSystem.DTOs;
using StockManagementSystem.Models;
namespace StockManagementSystem.Repositories
{
    public interface ICustomerRepository
    {
        Task<mResult<IEnumerable<Customer>>> GetAllCustomersAsync();
        Task<mResult<Customer?>> GetCustomerByIdAsync(int id);
        Task<mResult<bool>> AddCustomerAsync(Customer customer);
        Task<mResult<bool>> UpdateCustomerAsync(Customer customer);
        Task<mResult<bool>> DeleteCustomerAsync(int id);
    }
}
