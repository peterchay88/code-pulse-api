using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController: ControllerBase
{
    private readonly ApplicationDbContext dbContext;
    
    public CategoriesController(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequestDto request)
    {
        // Map DTO from request to category domain model
        var category = new Category
        {
            Name = request.Name,
            UrlHandle = request.UrlHandle
        };
        
        await dbContext.Categories.AddAsync(category);
        await dbContext.SaveChangesAsync();
        
        // Map domain model to a DTO to pass along to the user
        var response = new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            UrlHandle = category.UrlHandle
        };
        
        return Ok(response);
    }
}