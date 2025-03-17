using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Multi_Level_Blogging_System.Common.Enums;
using Multi_Level_Blogging_System.Models;
using Multi_Level_Blogging_System.Models.Request;
using Multi_Level_Blogging_System.Common.helper;
using Multi_Level_Blogging_System.Extensions;
using Multi_Level_Blogging_System.Models.Response;

namespace Multi_Level_Blogging_System.Controllers;


[ApiController]
[Route("api/[controller]")]
public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<User> _signInManager;
    // GET
    public AccountController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        
    }
    
    [HttpPost("registration")]
    public async Task<IActionResult> Register([FromBody] Register register)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        register.FirstName.Required(nameof(register.FirstName));
        register.Email.Required(nameof(register.Email));
        register.Password.Required(nameof(register.Password));
        register.ConfirmPassword.Required(nameof(register.ConfirmPassword));
        register.Email.EmailValidation();
        
        if (!register.Password.Equals(register.ConfirmPassword)) 
            return BadRequest(new { message = "Passwords and confirmation password do not match." });
        
        var hashedPassword = PasswordHash.HashPassword(register.Password);
        var user = new User()
        {
            FirstName = register.FirstName,
            LastName = register.LastName,
            UserName = register.Email,
            Email = register.Email,
            UserType = register.UserType,
            Password = hashedPassword,
        };
    
        var result = await _userManager.CreateAsync(user);
    
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }
        var role = register.UserType.ToString();
        var roleExists = await _roleManager.RoleExistsAsync(role);
        
        if (!roleExists)
        {
            await _roleManager.CreateAsync(new IdentityRole(role));
        }
        
        var roleResult = await _userManager.AddToRoleAsync(user, role);
        
        if (!roleResult.Succeeded)
        {
            return BadRequest(roleResult.Errors);
        }
    
        return Ok(new { Message = "User registered successfully!" });
    }
    
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] Login login)
    {
        login.Email.Required(nameof(login.Email));
        login.Password.Required(nameof(login.Password));
        login.Email.EmailValidation();
        var user = await _userManager.FindByEmailAsync(login.Email);

        if (user == null)
        {
            return Unauthorized(new { message = "User not found!" });
        }
        
        var isPasswordMatch = PasswordHash.VerifyPassword(login.Password, user.Password);

        // var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
        //
        // if (!result.Succeeded)
        // {
        //     return Unauthorized(new { message = "Invalid credentials!" });
        // }

        if (!isPasswordMatch)
        {
            return Unauthorized(new { message = "Invalid credentials!"});
        }

        var token = GenerateToken.GenerateJwtToken(user);

        return Ok(new { message = "Login successful!", token });
    }
    
    
}