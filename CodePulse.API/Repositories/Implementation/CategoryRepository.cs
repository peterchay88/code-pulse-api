using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation;

public class CategoryRepository(ApplicationDbContext dbContext) : ICategoryRepository
{
    public async Task<Category> CreateAsync(Category category)
    {
        await dbContext.Categories.AddAsync(category);
        await dbContext.SaveChangesAsync();
        
        return category;
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        var categories = await dbContext.Categories.ToListAsync();
        return categories;
    }

    public async Task<Category?> GetByIdAsync(Guid id)
    {
        var category = await dbContext.Categories
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync();
        return category;
    }
}