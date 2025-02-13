using CodePluse.API.Data;
using CodePluse.API.Models.Domain;
using CodePluse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePluse.API.Repositories.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDBContext dBContext;

        public CategoryRepository(ApplicationDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public async Task<Category> CreateAsync(Category category)
        {
            await dBContext.Categories.AddAsync(category);
            await dBContext.SaveChangesAsync();
            return category;
        }       

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await dBContext.Categories.ToListAsync();            
        }

        public async Task<Category?> GetByIdAsync(Guid id)
        {
            return await dBContext.Categories.FirstOrDefaultAsync(x => x.id == id);
        }

        public async Task<Category?> UpdateAsync(Category category)
        {
            var existingCategory = await dBContext.Categories.FirstOrDefaultAsync(x => x.id == category.id);

            if(existingCategory != null)
            {
                dBContext.Entry(existingCategory).CurrentValues.SetValues(category);
                await dBContext.SaveChangesAsync();
                return category;
            }
            return null;
        }

        public async Task<Category?> DeleteAsync(Guid id)
        {
            var existingCategory = await dBContext.Categories.FirstOrDefaultAsync(x => x.id == id);

            if (existingCategory != null) {
                dBContext.Categories.Remove(existingCategory);
                dBContext.SaveChangesAsync();
                return existingCategory;
            }
            return null;
        }
    }
}
