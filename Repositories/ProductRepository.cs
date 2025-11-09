
using Microsoft.EntityFrameworkCore;
using StockManagementSystem.Data;
using StockManagementSystem.DTOs;
using StockManagementSystem.Models;

namespace StockManagementSystem.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StockDbContext _context;

        public ProductRepository(StockDbContext context)
        {
            _context = context;
        }

        public async Task<mResult<IEnumerable<Product>>> GetAllProductsAsync()
        {
            try
            {
                var products = await _context.Products.ToListAsync();
                return new mResult<IEnumerable<Product>>(true, "Products retrieved successfully", products);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving products", ex);
            }
        }

        public async Task<mResult<Product?>> GetProductByIdAsync(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                return new mResult<Product?>(true, "Product retrieved successfully", product);
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                throw new Exception("Error retrieving product", ex);
            }
        }

        public async Task<mResult<bool>> AddProductAsync(Product product)
        {
            try
            {
                product.CreatedAt = DateTime.Now;
                product.UpdatedAt = DateTime.Now;
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
                return new mResult<bool>(true, "Product added successfully", true);
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding product", ex);
            }
        }

        public async Task<mResult<bool>> UpdateProductAsync(Product product)
        {
            try
            {
                product.UpdatedAt = DateTime.Now;
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
                return new mResult<bool>(true, "Product updated successfully", true);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating product", ex);
            }
        }

        public async Task<mResult<bool>> DeleteProductAsync(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product != null)
                {
                    _context.Products.Remove(product);
                    await _context.SaveChangesAsync();
                    return new mResult<bool>(true, "Product deleted successfully", true);
                }

                return new mResult<bool>(false, "Product not found", false);
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting product", ex);
            }
        }

        public async Task<mResult<IEnumerable<mCodeDesc>>> GetProductCodesAndDescriptionsAsync()
        {
            try
            {
                var codeDescs = await _context.Products
                    .Select(p => new mCodeDesc(p.Id.ToString(), p.ProductCode + " - " + p.Name))
                    .ToListAsync();

                return new mResult<IEnumerable<mCodeDesc>>(true, "Product codes and descriptions retrieved successfully", codeDescs);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving product codes and descriptions", ex);
            }
        }
    }
}