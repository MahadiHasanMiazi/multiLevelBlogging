using Microsoft.AspNetCore.Identity;

namespace Multi_Level_Blogging_System.Models;

public class Roles : IdentityRole
{
    public string Id { get; set; }
    public string Name { get; set; }
}