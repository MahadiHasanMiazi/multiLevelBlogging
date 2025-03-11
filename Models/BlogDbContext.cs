using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Multi_Level_Blogging_System.Models.Response;


namespace Multi_Level_Blogging_System.Models;

public class BlogDbContext : IdentityDbContext<User>
{
    protected readonly IConfiguration _configuration;
    
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Comment> Comments { get; set; }
    // public DbSet<BlogCategory> BlogCategories { get; set; }
    // public DbSet<BlogComment> BlogComments { get; set; }

    public BlogDbContext(DbContextOptions<BlogDbContext> options): base(options)
    {
        // _configuration = configuration;
        
    }

    // protected override void OnConfiguring(ModelBuilder optionsBuilder) 
    // {
    //     // if (optionsBuilder.IsConfigured) return;
    //     // var connectionString = _configuration.GetConnectionString("SampleConnection");
    //     // optionsBuilder.UseNpgsql(connectionString);
    //     
    //     
    // }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); // Important for Identity tables

        // Additional configurations
        modelBuilder.Entity<User>().ToTable("Users");
        // modelBuilder.Entity<Blog>().ToTable("Blogs");
        
        modelBuilder.Entity<Blog>()
            .Property(b => b.Id)
            .ValueGeneratedOnAdd(); // Auto-incremented ID

        modelBuilder.Entity<Category>()
            .Property(b => b.Id)
            .ValueGeneratedOnAdd();
        
        modelBuilder.Entity<Blog>()
            .HasOne(p => p.Category)
            .WithMany(b => b.Blogs)
            .HasForeignKey(f => f.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Comment>()
            .Property(b => b.Id)
            .ValueGeneratedOnAdd();
        
        modelBuilder.Entity<Comment>()
            .HasOne(p => p.Blog)
            .WithMany(b => b.Comments)
            .HasForeignKey(f => f.BlogId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.HasDefaultSchema("multiLevelBlogging");

    }
    
   
    
    
}