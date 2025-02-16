using Microsoft.EntityFrameworkCore;
using Multi_Level_Blogging_System.Models;

namespace Multi_Level_Blogging_System.Extensions;

public static class MigrationExtensions
{
    public static void ApplyAllMigrations(this IApplicationBuilder app)
    {
        using IServiceScope serviceScope = app.ApplicationServices.CreateScope();
        using BlogDbContext context = serviceScope.ServiceProvider.GetRequiredService<BlogDbContext>();
        context.Database.Migrate();
    }
}