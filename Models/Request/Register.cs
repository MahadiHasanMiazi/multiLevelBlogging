using Multi_Level_Blogging_System.Common.Enums;

namespace Multi_Level_Blogging_System.Models.Request;

public class Register
{
    public string FirstName { get; set; }
    public  string LastName { get; set; }
    public string Email { get; set; }
    
    public UserType UserType { get; set; }
    
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}