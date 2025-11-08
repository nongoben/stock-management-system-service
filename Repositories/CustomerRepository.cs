
using Microsoft.EntityFrameworkCore;
using StockManagementSystem.Data;
using StockManagementSystem.DTOs;
using StockManagementSystem.Models;

namespace StockManagementSystem.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly StockDbContext _context;

        public CustomerRepository(StockDbContext context)
        {
            _context = context;
        }

        public async Task<mResult<IEnumerable<Customer>>> GetAllCustomersAsync()
        {
            try
            {
                var customers = await _context.Customers.ToListAsync();
                return new mResult<IEnumerable<Customer>>(true, "Customers retrieved successfully", customers);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving customers", ex);
            }
        }

        public async Task<mResult<Customer?>> GetCustomerByIdAsync(int id)
        {
            try
            {
                var customer = await _context.Customers.FindAsync(id);
                return new mResult<Customer?>(true, "Customer retrieved successfully", customer);
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                throw new Exception("Error retrieving customer", ex);
            }
        }

        public async Task<mResult<bool>> AddCustomerAsync(Customer customer)
        {
            try
            {
                await _context.Customers.AddAsync(customer);
                await _context.SaveChangesAsync();
                return new mResult<bool>(true, "Customer added successfully", true);
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding customer", ex);
            }
        }

        public async Task<mResult<bool>> UpdateCustomerAsync(Customer customer)
        {
            try
            {
                _context.Customers.Update(customer);
                await _context.SaveChangesAsync();
                return new mResult<bool>(true, "Customer updated successfully", true);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating product", ex);
            }
        }

        public async Task<mResult<bool>> DeleteCustomerAsync(int id)
        {
            try
            {
                var customer = await _context.Customers.FindAsync(id);
                if (customer != null)
                {
                    _context.Customers.Remove(customer);
                    await _context.SaveChangesAsync();
                    return new mResult<bool>(true, "Customer deleted successfully", true);
                }

                return new mResult<bool>(false, "Customer not found", false);
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting customer", ex);
            }
        }
    }
}