using Cleaning_Hup.Abstraction;
using Cleaning_Hup.Contracts.Reponse;
using Cleaning_Hup.Contracts.Request;
using Cleaning_Hup.Models;
using Cleaning_Hup.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Cleaning_Hup.Services.Classes
{
    public class CustomerService : ICustomerService
    {
        private readonly AppDbContext _context;

        public CustomerService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CustomerResponse>> GetAllAsync()
        {
            return await _context.Customers
                .Select(c => new CustomerResponse
                {
                    Id = c.Id,
                    Name = c.Name,
                    Phone = c.Phone,
                    Balance = c.Balance,
                    CreatedAt = c.CreatedAt
                }).ToListAsync();
        }

        public async Task<CustomerResponse?> GetByIdAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return null;
            return new CustomerResponse { Id = customer.Id, Name = customer.Name, Phone = customer.Phone, Balance = customer.Balance, CreatedAt = customer.CreatedAt };
        }

        public async Task<CustomerResponse> CreateAsync(CustomerRequest request)
        {
            var customer = new Customer { Name = request.Name, Phone = request.Phone };
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return new CustomerResponse { Id = customer.Id, Name = customer.Name, Phone = customer.Phone, Balance = customer.Balance, CreatedAt = customer.CreatedAt };
        }

        public async Task<CustomerResponse?> UpdateAsync(int id, CustomerRequest request)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return null;
            customer.Name = request.Name;
            customer.Phone = request.Phone;
            await _context.SaveChangesAsync();
            return new CustomerResponse { Id = customer.Id, Name = customer.Name, Phone = customer.Phone, Balance = customer.Balance, CreatedAt = customer.CreatedAt };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return false;
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return true;
        }

        
    }
}

