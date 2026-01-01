# Copilot Instructions for ASP.NET Core URL Shortener

## Project Overview

This is an ASP.NET Core MVC URL shortener web application that demonstrates basic CRUD operations and URL redirection patterns. The application converts numeric IDs to short alphanumeric strings using base-62 encoding with a custom alphabet.

## Technology Stack

- **Framework**: ASP.NET Core 2.1 MVC
- **Language**: C# (.NET Core)
- **Database**: SQLite with Entity Framework Core 2.1
- **Frontend**: MVC Razor Views, Bootstrap, jQuery (via Bower)
- **Build System**: .NET CLI (`dotnet`)

## Architecture and Structure

### Project Layout
- `Controllers/` - MVC controllers (HomeController, ShortUrlsController)
- `Models/` - Data models (ShortUrl entity)
- `Services/` - Business logic services (ShortUrlService)
- `Helpers/` - Utility classes (ShortUrlHelper for base-62 encoding)
- `Data/` - Entity Framework DbContext (UrlShortenerContext)
- `Views/` - Razor view templates
- `Migrations/` - Entity Framework database migrations
- `wwwroot/` - Static files (CSS, JS, images)

### Key Components

1. **ShortUrlHelper** (`Helpers/ShortUrlHelper.cs`)
   - Provides bijective conversion between numeric IDs and short strings using a 51-character alphabet
   - Uses custom alphabet: "23456789bcdfghjkmnpqrstvwxyzBCDFGHJKLMNPQRSTVWXYZ-_"
   - Avoids offensive words (removed vowels) and ambiguous characters (removed I, l, 1, O, 0)
   - Third-party code from delight.im (MIT licensed)

2. **ShortUrlService** (`Services/ShortUrlService.cs`)
   - Implements IShortUrlService interface
   - Handles CRUD operations for ShortUrl entities
   - Methods: GetById, GetByPath, GetByOriginalUrl, Save

3. **ShortUrlsController** (`Controllers/ShortUrlsController.cs`)
   - Manages URL creation and redirection
   - Routes: Create (GET/POST), Show, RedirectTo
   - Uses dependency injection for IShortUrlService

4. **UrlShortenerContext** (`Data/UrlShortenerContext.cs`)
   - Entity Framework DbContext
   - Manages ShortUrls DbSet
   - Configured with SQLite in Startup.cs

## Coding Conventions

### General C# Style
- Use PascalCase for public members, classes, and methods
- Use camelCase with underscore prefix (_) for private fields
- Use `var` for local variable declarations when type is obvious
- Include `using` statements at the top of files, organized alphabetically

### Patterns and Practices
- **Dependency Injection**: Services are registered in `Startup.ConfigureServices()` and injected via constructor
- **Service Layer**: Business logic is in service classes, not controllers
- **Interface-based Services**: Use interfaces (e.g., IShortUrlService) for testability
- **MVC Pattern**: Strict separation between Models, Views, and Controllers
- **Data Annotations**: Use attributes like `[Required]` for model validation
- **Anti-Forgery Tokens**: Use `[ValidateAntiForgeryToken]` on POST actions

### Database and Entity Framework
- DbContext is scoped per request
- Use `DbSet<T>` for entity collections
- Database file: `shorturls.db` (SQLite)
- Migrations are in `Migrations/` folder

## Build and Development Commands

### Setup
```bash
# Restore dependencies
dotnet restore

# Initialize database schema
dotnet ef database update
```

### Development
```bash
# Build the project
dotnet build

# Run the application (defaults to http://localhost:5000)
dotnet run

# Run tests (if any exist)
dotnet test
```

### Database Migrations
```bash
# Create a new migration
dotnet ef migrations add <MigrationName>

# Apply migrations to database
dotnet ef database update

# Revert to a previous migration
dotnet ef database update <MigrationName>
```

## Important Notes

### URL Shortening Algorithm
- Numeric IDs are converted to short strings using ShortUrlHelper with a 51-character alphabet
- Encoding: `int id` → `string shortCode`
- Decoding: `string shortCode` → `int id`
- The algorithm is bijective (one-to-one mapping)

### Configuration
- SQLite database connection string is configured in the `ConfigureServices` method of `Startup.cs` (default: `"filename=shorturls.db"`)
- App settings are in `appsettings.json` and `appsettings.Development.json`
- Cookie policy requires user consent (GDPR compliance)

### Routes
- Default route: redirects to ShortUrls/Create
- Short URL pattern: `/ShortUrls/RedirectTo/{path}`
- Named route: "ShortUrls_RedirectTo"

## When Making Changes

### Adding New Features
- Follow the existing MVC pattern
- Add services to `Services/` folder with interfaces
- Register services in `Startup.ConfigureServices()`
- Use dependency injection for all dependencies
- Add views to appropriate folders in `Views/`

### Modifying Database Schema
- Create Entity Framework migrations for any model changes
- Update the database using `dotnet ef database update`
- Don't modify migration files after they've been applied

### Security Considerations
- Always use `[ValidateAntiForgeryToken]` on state-changing actions
- Validate user input with data annotations
- Use HTTPS redirection (enabled by default)
- Never expose database connection strings in client-side code

### Testing
- The project currently has no test infrastructure
- When adding tests, use xUnit (common for .NET Core projects)
- Mock IShortUrlService for controller tests
- Test the ShortUrlHelper encoding/decoding separately

## Common Tasks

### Add a New Controller Action
1. Add method to controller with appropriate HTTP verb attribute
2. Return View() with model if needed
3. Add corresponding Razor view in `Views/{ControllerName}/`
4. Update routes if using custom routing

### Add a New Service
1. Create interface in `Services/` (e.g., IMyService.cs)
2. Create implementation (e.g., MyService.cs)
3. Register in `Startup.ConfigureServices()`: `services.AddScoped<IMyService, MyService>()`
4. Inject via constructor where needed

### Modify the ShortUrl Model
1. Update `Models/ShortUrl.cs`
2. Create migration: `dotnet ef migrations add <DescriptiveName>`
3. Apply migration: `dotnet ef database update`
4. Update views and services as needed

## Dependencies

Key NuGet packages:
- `Microsoft.AspNetCore.App` (2.1.22) - ASP.NET Core metapackage
- `Microsoft.EntityFrameworkCore.Sqlite` (2.1.1) - SQLite database provider
- `BuildBundlerMinifier` (2.8.391) - CSS/JS bundling and minification
- `Microsoft.VisualStudio.Web.BrowserLink` (2.1.1) - Development-time browser sync

## Resources

- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core Documentation](https://docs.microsoft.com/en-us/ef/core/)
- [URL Shortening Algorithm](https://stackoverflow.com/questions/742013/how-to-code-a-url-shortener)
- [ShortURL Library](https://github.com/delight-im/ShortURL)
