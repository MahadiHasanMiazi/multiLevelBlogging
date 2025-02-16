using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Multi_Level_Blogging_System.Models;

public class BlogDbContext : IdentityDbContext<User>
{
    protected readonly IConfiguration _configuration;

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
        modelBuilder.Entity<Blog>().ToTable("Blogs");

        modelBuilder.HasDefaultSchema("multiLevelBlogging");

    }
    
}