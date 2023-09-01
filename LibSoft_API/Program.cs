using LibSoft_API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<LibSoftDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("connection")));
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();