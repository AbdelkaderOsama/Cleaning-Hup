using AutoMapper;
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
        private readonly IMapper _mapper;

        public CustomerService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerResponse>> GetAllAsync()
        {
            var customers = await _context.Customers.ToListAsync();
            return _mapper.Map<IEnumerable<CustomerResponse>>(customers);
        }

        public async Task<CustomerResponse?> GetByIdAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return null;
            return _mapper.Map<CustomerResponse>(customer);
        }

        public async Task<CustomerResponse> CreateAsync(CustomerRequest request)
        {
            var customer = _mapper.Map<Customer>(request);
            customer.CreatedAt = DateTime.UtcNow;
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return _mapper.Map<CustomerResponse>(customer);
        }

        public async Task<CustomerResponse?> UpdateAsync(int id, CustomerRequest request)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return null;
            customer.Name = request.Name;
            customer.Phone = request.Phone;
            await _context.SaveChangesAsync();
            return _mapper.Map<CustomerResponse>(customer);
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

