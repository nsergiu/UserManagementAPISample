var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Register the user service
builder.Services.AddSingleton<UserManagementAPI.Services.IUserService, UserManagementAPI.Services.UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Add exception handling middleware FIRST
app.UseMiddleware<UserManagementAPI.Middleware.ExceptionHandlingMiddleware>();

// Add token validation middleware
app.UseMiddleware<UserManagementAPI.Middleware.TokenValidationMiddleware>();

// Add request/response logging middleware
app.UseMiddleware<UserManagementAPI.Middleware.RequestResponseLoggingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
