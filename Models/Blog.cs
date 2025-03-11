namespace Multi_Level_Blogging_System.Models;

public class Blog
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt {get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public ICollection<Comment> Comments { get; set; }
}