
using Microsoft.EntityFrameworkCore;
using StockManagementSystem.Data;
using StockManagementSystem.DTOs;
using StockManagementSystem.Models;

namespace StockManagementSystem.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly StockDbContext _context;

        public OrderRepository(StockDbContext context)
        {
            _context = context;
        }

        public async Task<mResult<IEnumerable<Order>>> GetAllOrdersAsync(string? product, string? customer, string? salesPerson, DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                var query = _context.Orders.Include(o => o.Product).AsQueryable();
                if (!string.IsNullOrEmpty(product))
                {
                    query = query.Where(o => o.ProductId.ToString().Contains(product));
                }
                if (!string.IsNullOrEmpty(customer))
                {
                    query = query.Where(o => o.CustomerName.Contains(customer));
                }
                if (!string.IsNullOrEmpty(salesPerson))
                {
                    query = query.Where(o => o.SalesPerson.Contains(salesPerson));
                }
                if (fromDate.HasValue)
                {
                    query = query.Where(o => o.OrderDate >= fromDate.Value);
                }
                if (toDate.HasValue)
                {
                    query = query.Where(o => o.OrderDate <= toDate.Value);
                }
                var orders = await query.ToListAsync();
                return new mResult<IEnumerable<Order>>(true, "Orders retrieved successfully", orders);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving orders", ex);
            }
        }

        public async Task<mResult<Order?>> GetOrderByIdAsync(int id)
        {
            try
            {
                var order = await _context.Orders.FindAsync(id);
                return new mResult<Order?>(true, "Order retrieved successfully", order);
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                throw new Exception("Error retrieving order", ex);
            }
        }

        public async Task<mResult<bool>> AddOrderAsync(Order order)
        {
            try
            {
                var stockItem = await _context.Products.FindAsync(order.ProductId);
                if (stockItem == null)
                {
                    return new mResult<bool>(false, "Product not found", false);
                }

                if (stockItem.Quantity < order.Quantity)
                {
                    return new mResult<bool>(false, "Insufficient stock quantity", false);
                }

                stockItem.UpdatedAt = DateTime.Now;
                stockItem.Quantity -= order.Quantity;
                stockItem.SoldQuantity += order.Quantity;
                _context.Products.Update(stockItem);

                order.OrderDate = DateTime.Now;
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
                return new mResult<bool>(true, "Order added successfully", true);
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding order", ex);
            }
        }

        public async Task<mResult<bool>> UpdateOrderAsync(Order order)
        {
            try
            {
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
                return new mResult<bool>(true, "Order updated successfully", true);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating product", ex);
            }
        }

        public async Task<mResult<bool>> DeleteOrderAsync(int id)
        {
            try
            {
                var order = await _context.Orders.FindAsync(id);
                if (order != null)
                {
                    _context.Orders.Remove(order);
                    await _context.SaveChangesAsync();
                    return new mResult<bool>(true, "Order deleted successfully", true);
                }

                return new mResult<bool>(false, "Order not found", false);
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting order", ex);
            }
        }

        public Task<mResult<IEnumerable<mCodeDesc>>> GetSalesPersonsAsync()
        {
            try
            {
                var salesPersons = _context.Orders
                    .Select(o => o.SalesPerson)
                    .Distinct()
                    .Where(sp => sp != null)
                    .Select(sp => new mCodeDesc(sp!, sp!))
                    .ToList();

                return Task.FromResult(new mResult<IEnumerable<mCodeDesc>>(true, "Sales persons retrieved successfully", salesPersons));
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving sales persons", ex);
            }
        }

        public Task<mResult<IEnumerable<mCodeDesc>>> GetCustomersAsync()
        {
            try
            {
                var customers = _context.Orders
                    .Select(o => o.CustomerName)
                    .Distinct()
                    .Where(c => c != null)
                    .Select(c => new mCodeDesc(c!, c!))
                    .ToList();

                return Task.FromResult(new mResult<IEnumerable<mCodeDesc>>(true, "Customers retrieved successfully", customers));
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving customers", ex);
            }
        }
    }
}