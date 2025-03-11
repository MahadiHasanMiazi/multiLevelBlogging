namespace Multi_Level_Blogging_System.Models;

public class Comment
{
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int BlogId { get; set; }
    public Blog Blog { get; set; }
}