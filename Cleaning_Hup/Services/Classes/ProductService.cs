//using Cleaning_Hup.Abstraction;
//using Cleaning_Hup.Contracts.Reponse;
//using Cleaning_Hup.Contracts.Request;
//using Cleaning_Hup.Models;
//using Cleaning_Hup.Persistance;
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

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductResponse>> GetAllAsync()
        {
            return await _context.Products
                .Include(p => p.Category)
                .Select(p => new ProductResponse
                {
                    Id = p.Id,
                    Name = p.Name,
                    CategoryName = p.Category.Name,
                    Unit = p.Unit,
                    WholesalePrice = p.WholesalePrice,
                    RetailPrice = p.RetailPrice,
                    CreatedAt = p.CreatedAt
                }).ToListAsync();
        }

        public async Task<ProductResponse?> GetByIdAsync(int id)
        {
            var product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) return null;
            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                CategoryName = product.Category.Name,
                Unit = product.Unit,
                WholesalePrice = product.WholesalePrice,
                RetailPrice = product.RetailPrice,
                CreatedAt = product.CreatedAt
            };
        }

        public async Task<ProductResponse> CreateAsync(ProductRequest request)
        {
            var product = new Product
            {
                Name = request.Name,
                CategoryId = request.CategoryId,
                Unit = request.Unit,
                WholesalePrice = request.WholesalePrice,
                RetailPrice = request.RetailPrice
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            var category = await _context.Categories.FindAsync(product.CategoryId);
            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                CategoryName = category?.Name ?? "",
                Unit = product.Unit,
                WholesalePrice = product.WholesalePrice,
                RetailPrice = product.RetailPrice,
                CreatedAt = product.CreatedAt
            };
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
            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                CategoryName = product.Category.Name,
                Unit = product.Unit,
                WholesalePrice = product.WholesalePrice,
                RetailPrice = product.RetailPrice,
                CreatedAt = product.CreatedAt
            };
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

