using FluentValidation;
using LibSoft_API.Data;
using LibSoft_API.Endpoints;
using LibSoft_API.MappingProfiles;
using LibSoft_API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<LibSoftDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("connection")));
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddAutoMapper(typeof(BookMappingConfig));
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.AddBookEndspoints();
app.Run();