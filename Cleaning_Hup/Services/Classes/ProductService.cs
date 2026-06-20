//using Cleaning_Hup.Abstraction;
//using Cleaning_Hup.Contracts.Reponse;
//using Cleaning_Hup.Contracts.Request;
//using Cleaning_Hup.Models;
//using Cleaning_Hup.Persistance;
using AutoMapper;
using Cleaning_Hup.Abstraction;
using Cleaning_Hup.Contracts;
using Cleaning_Hup.Contracts.Reponse;
using Cleaning_Hup.Contracts.Request;
using Cleaning_Hup.Models;
using Cleaning_Hup.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Cleaning_Hup.Services.Classes
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductResponse>> GetAllAsync()
        {
            var products = await _context.Products.Include(p => p.Category).ToListAsync();
            return _mapper.Map<IEnumerable<ProductResponse>>(products);
        }

        public async Task<ProductResponse?> GetByIdAsync(int id)
        {
            var product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) return null;
            return _mapper.Map<ProductResponse>(product);
        }

        public async Task<ProductResponse> CreateAsync(ProductRequest request)
        {
            var product = _mapper.Map<Product>(request);
            product.CreatedAt = DateTime.UtcNow;
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            var saved = await _context.Products.Include(p => p.Category).FirstAsync(p => p.Id == product.Id);
            return _mapper.Map<ProductResponse>(saved);
        }

        public async Task<ProductResponse?> UpdateAsync(int id, ProductRequest request)
        {
            var product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) return null;
            product.Name = request.Name;
            product.CategoryId = request.CategoryId;
            product.Unit = request.Unit;
            product.WholesalePrice = request.WholesalePrice;
            product.RetailPrice = request.RetailPrice;
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductResponse>(product);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

