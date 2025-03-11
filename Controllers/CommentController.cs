using Microsoft.AspNetCore.Mvc;
using Multi_Level_Blogging_System.Models;
using Multi_Level_Blogging_System.Models.Request;

namespace Multi_Level_Blogging_System.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentController : Controller
{
    private readonly BlogDbContext _context;

    public CommentController(BlogDbContext context)
    {
        _context = context;
    }

    [HttpPost("create")]
    public IActionResult CreateComment([FromBody] CommentRequestDto comment)
    {
        try
        {
            var blog = _context.Blogs.Any(b => b.Id == comment.BlogId);
            if (!blog)
            {
                return BadRequest(new { Message = "Blog not found" });
            }

            var newComment = new Comment()
            {
                BlogId = comment.BlogId,
                Content = comment.Content,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Comments.Add(newComment);
            _context.SaveChanges();

            return Ok(new { Message = "Comment created" });
        }
        catch (Exception e)
        {
            return BadRequest(new { Message = e.Message });
        }
        
    }
}