# Clone the repository
git clone https://github.com/MahadiHasanMiazi/multiLevelBlogging.git

cd multiLevelBlogging

# Restore dependencies
dotnet restore

# Apply EF Core migrations
dotnet ef database update

# Run the application
dotnet run
