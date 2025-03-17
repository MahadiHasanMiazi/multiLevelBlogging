using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Multi_Level_Blogging_System.Common.helper;
using Multi_Level_Blogging_System.Extensions;
using Multi_Level_Blogging_System.Middleware;
using Multi_Level_Blogging_System.Models;
using Multi_Level_Blogging_System.Seeders;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// builder.Services.AddAuthentication()
//     .AddCookie(IdentityConstants.ApplicationScheme)
//     .AddBearerToken(IdentityConstants.BearerScheme);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false; // Set to true in production
        options.SaveToken = true;
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsASecretKeyThatIsAtLeast32CharsLong!")),
            ValidIssuer = "https://localhost:5067", // Change this to your actual issuer
            ValidAudience = "https://localhost:5067", // Change this to your actual audience
            // IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
            //     System.Text.Encoding.UTF8.GetBytes("ThisIsASecretKeyThatIsAtLeast32CharsLong!")) // Change this key
        };
    });
builder.Services.AddAuthorization();

builder.Services.AddIdentityCore<User>(options =>
    {
        options.User.RequireUniqueEmail = true;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<BlogDbContext>()
    .AddApiEndpoints();


builder.Services.AddDbContext<BlogDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DatabaseConnection")));

builder.Services.AddHostedService<RoleSeeder>();
// builder.Services.AddTransient<IPasswordValidator<User>, CustomPasswordValidation>();



// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Auth API",
        Version = "v1"
    });
    
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer {your_token}'"
    });

    // ✅ Require Token in API Requests
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
    
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    app.ApplyAllMigrations();
}

app.UseHttpsRedirection();
app.MapIdentityApi<User>();
app.UseAuthentication(); // ⬅️ Must come before UseAuthorization()
app.UseAuthorization();
app.MapControllers();
app.UseMiddleware<UnauthorizedMiddleware>();




var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast")
    .WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
