using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using ToDoApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDBContext>(options=>{
    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    }
    else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
    {
        // Use SQLite on macOS
        options.UseSqlite(builder.Configuration.GetConnectionString("SQLiteConnection"));
    }
    else
    {
        throw new PlatformNotSupportedException("Unsupported platform");
    }
});
builder.Services.AddCors(options =>
{
    options.AddPolicy(name:"FrontEndUI", policy =>
    {
        policy.WithOrigins("http://localhost:4200/").AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("FrontEndUI");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
