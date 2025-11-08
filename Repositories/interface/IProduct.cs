using StockManagementSystem.DTOs;
using StockManagementSystem.Models;
namespace StockManagementSystem.Repositories
{
    public interface IProductRepository
    {
        Task<mResult<IEnumerable<Product>>> GetAllProductsAsync();
        Task<mResult<Product?>> GetProductByIdAsync(int id);
        Task<mResult<bool>> AddProductAsync(Product product);
        Task<mResult<bool>> UpdateProductAsync(Product product);
        Task<mResult<bool>> DeleteProductAsync(int id);
    }
}
