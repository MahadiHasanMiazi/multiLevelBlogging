namespace Multi_Level_Blogging_System.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Blog> Blogs { get; set; }
    
}