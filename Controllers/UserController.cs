using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Multi_Level_Blogging_System.Models;

namespace Multi_Level_Blogging_System.Controllers;


[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserManager<User> _userManager;

    public UserController(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    
    
    

    [HttpPut("update-user/{id}")]
    public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserDto model)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound(new { message = "User not found" });
        }

        // Update user properties
        user.Email = model.Email ?? user.Email;
        user.PhoneNumber = model.PhoneNumber ?? user.PhoneNumber;

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok(new { message = "User updated successfully" });
    }
}

// DTO to receive user update data
public class UpdateUserDto
{
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
}