using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController(ICategoryRepository categoryRepository) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequestDto request)
    {
        // Map DTO from request to category domain model
        var category = new Category
        {
            Name = request.Name,
            UrlHandle = request.UrlHandle
        };
        
        await categoryRepository.CreateAsync(category);
        
        // Map domain model to a DTO to pass along to the user
        var response = new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            UrlHandle = category.UrlHandle
        };
        
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await categoryRepository.GetAllAsync();
        var getCategoriesList = new List<CategoryDto>();

        foreach (var category in categories)
        {
            var getCategoryRequest = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };
            getCategoriesList.Add(getCategoryRequest);
        }
        
        return Ok(getCategoriesList);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetCategoryById([FromRoute] Guid id)
    {
        var category = await categoryRepository.GetByIdAsync(id);
        
        if(category == null)
        {
            return NoContent();
        }

        var response = new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            UrlHandle = category.UrlHandle
        };
        
        return Ok(response);
    }
}