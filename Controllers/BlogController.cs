using Microsoft.AspNetCore.Mvc;

namespace Multi_Level_Blogging_System.Controllers;

public class BlogController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}