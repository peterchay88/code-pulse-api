using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Data;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    public DbSet<Models.Domain.BlogPost> BlogPosts { get; set; }
    public DbSet<Models.Domain.Category> Categories { get; set; }
}