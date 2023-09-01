using System.Net;
using AutoMapper;
using FluentValidation;
using LibSoft_API.Data;
using LibSoft_API.MappingProfiles;
using LibSoft_API.Services;
using LibSoft_Models;
using LibSoft_Models.API_Model_Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<LibSoftDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("connection")));
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddAutoMapper(typeof(BookMappingConfig));
builder.Services.AddTransient<IBookRepository, BookRepository>();

var app = builder.Build();

app.MapGet("/api/book",async (IBookRepository repo) =>
{
    APIResponse response = new APIResponse();
    response.Result = await repo.GetAllBooks();
    response.IsSuccess = true;
    response.StatusCode = HttpStatusCode.OK;
    return Results.Ok(response);
}).WithName("GetAllBooks").Produces<APIResponse>(200);

app.Run();