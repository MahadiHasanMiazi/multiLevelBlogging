using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Multi_Level_Blogging_System.Models;
using Multi_Level_Blogging_System.Models.Request;
using Multi_Level_Blogging_System.Models.Response;


namespace Multi_Level_Blogging_System.Controllers;



[ApiController]
[Route("api/[controller]")]
public class BlogController : Controller
{
    private readonly BlogDbContext _context;

    public BlogController(BlogDbContext context)
    {
        _context = context;
    }
    
    [HttpGet("blogList")]
    public List<BlogDTOResponse> Index()
    {
        var blogs = _context.Blogs.ToList();

        var blogList = blogs.Select(b => new BlogDTOResponse()
        {
            Id = b.Id,
            Title = b.Title,
            Content = b.Content
        }).ToList();
        
        return blogList.ToList();
    }
    
    [HttpPost("create")]
    [Authorize(Roles = "Admin,Editor")]
    public IActionResult CreateBlog([FromBody] BlogDTO blogDto)
    {
        try
        {
            var categoryOnDb = _context.Categories
                .FirstOrDefault(c => c.Name.ToLower() == blogDto.Category.ToLower());
            var categoryId = categoryOnDb?.Id ?? 0;
            if (categoryOnDb == null)
            {
                var category = new Category()
                {
                    Name = blogDto.Category.ToLower(),
                };
                _context.Categories.Add(category);
                _context.SaveChanges();
                categoryId = category.Id;
            }
        
            var blogPost = new Blog()
            {
                Title = blogDto.Title,
                Content = blogDto.Content,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                CategoryId = categoryId
            };
            _context.Blogs.Add(blogPost);
            _context.SaveChanges();
            return Ok(new {Message="Blog created"});
        }
        catch (Exception e)
        {
            return BadRequest(new { Message = e.Message });
        }
        
        
    }

    [HttpPut("update/{id}")]
    [Authorize(Roles = "Admin,Editor")]
    public IActionResult UpdateBlog(int id, [FromBody] BlogDTOResponse blogDto)
    {
        try
        {
            var blog = _context.Blogs.FirstOrDefault(b => b.Id == id);
            if (blog == null)
            {
                return BadRequest(new {message = "Blog not found"});
            }
            blog.Id = id;
            blog.Title = blogDto.Title ?? blog.Title;
            blog.Content = blogDto.Content ?? blog.Content;
            blog.UpdatedAt = DateTime.UtcNow;

            var result = _context.Update(blog);
            _context.SaveChanges();
        
            return Ok(new {Message = "Blog updated"});
        }
        catch (Exception e)
        {
            return BadRequest(new {Message = e.Message});
        }
        
    }

    [HttpGet("getBlog/{id}")]
    public IActionResult GetBlog(int id)
    {
        try
        {
            var blog = _context.Blogs.FirstOrDefault(b => b.Id == id);
            if (blog == null)
            {
                return BadRequest(new {message = "Blog not found"});
            }

            var blogDto = new BlogDTOResponse()
            {
                Id = blog.Id,
                Title = blog.Title,
                Content = blog.Content,
            };

            return Ok(blogDto);
        }
        catch (Exception e)
        {
            return BadRequest(new {Message = e.Message});
        }
        
    }

    [HttpDelete("delete/{id}")]
    [Authorize(Roles = "Admin,Editor")]
    public IActionResult DeleteBlog(int id)
    {
        var blog = _context.Blogs.FirstOrDefault(b => b.Id == id);
        if (blog == null)
        {
            return BadRequest(new {message = "Blog not found"});
        }
        
        _context.Blogs.Remove(blog);
        _context.SaveChanges();
        
        return Ok(new {Message = "Blog deleted"});
        
    }
    
}