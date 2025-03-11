using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Multi_Level_Blogging_System.Models;
using Multi_Level_Blogging_System.Models.Request;

namespace Multi_Level_Blogging_System.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : Controller
{
    private readonly BlogDbContext _context;

    public CategoryController(BlogDbContext context)
    {
        _context = context;
    }
    
    [HttpPost("create")]
    public IActionResult CreateCategory([FromBody] CategoryDTO categoryDto)
    {
        var categoryOnDb = _context.Categories
            .Any(c => c.Name.ToLower() == categoryDto.Name.ToLower());
        if (categoryOnDb)
        {
            return BadRequest(new {Message = "Category already exists"});
        }
        var category = new Category()
        {
            Name = categoryDto.Name,
        };
        _context.Categories.Add(category);
        _context.SaveChanges();
        return Ok(new {Message = "Category created"});
    }
}