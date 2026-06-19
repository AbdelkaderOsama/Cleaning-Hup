using Cleaning_Hup.Abstraction;
using Cleaning_Hup.Contracts.Reponse;
using Cleaning_Hup.Contracts.Request;
using Cleaning_Hup.Models;
using Cleaning_Hup.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Cleaning_Hup.Services.Classes
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoryResponse>> GetAllAsync()
        {
            return await _context.Categories
                .Select(c => new CategoryResponse
                {
                    Id = c.Id,
                    Name = c.Name,
                    CreatedAt = c.CreateAt
                }).ToListAsync();
        }

        public async Task<CategoryResponse?> GetByIdAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return null;
            return new CategoryResponse { Id = category.Id, Name = category.Name, CreatedAt = category.CreateAt };
        }

        public async Task<CategoryResponse> CreateAsync(CategoryRequest request)
        {
            var category = new Category
            {
                Name = request.Name,
                CreateAt = DateTime.UtcNow  // أضف السطر ده
            };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return new CategoryResponse { Id = category.Id, Name = category.Name, CreatedAt = category.CreateAt };
        }

        public async Task<CategoryResponse?> UpdateAsync(int id, CategoryRequest request)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return null;
            category.Name = request.Name;
            await _context.SaveChangesAsync();
            return new CategoryResponse { Id = category.Id, Name = category.Name, CreatedAt = category.CreateAt };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return false;
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}


