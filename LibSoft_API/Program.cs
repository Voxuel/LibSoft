using System.Net;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using LibSoft_API.Data;
using LibSoft_API.MappingProfiles;
using LibSoft_API.Services;
using LibSoft_Models;
using LibSoft_Models.API_Model_Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<LibSoftDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("connection")));
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddAutoMapper(typeof(BookMappingConfig));
builder.Services.AddScoped<IBookRepository, BookRepository>();

var app = builder.Build();

// Gets all books.
app.MapGet("/api/book",async (IBookRepository repo) =>
{
    var response = new APIResponse
    {
        Result = await repo.GetAllBooks(),
        IsSuccess = true,
        StatusCode = HttpStatusCode.OK
    };
    return Results.Ok(response);
}).WithName("GetAllBooks").Produces<APIResponse>(200);

// Gets book by id.
app.MapGet("/api/book/{id:int}", async (IBookRepository repo, int id) =>
{
    var response = new APIResponse();

    var result = await repo.GetBookById(id);
    if (result == null)
    {
        response.IsSuccess = false;
        response.StatusCode = HttpStatusCode.NotFound;
        response.ErrorMessages.Add("Invalid ID");
        return Results.NotFound(response);
    }
    response.IsSuccess = true;
    response.StatusCode = HttpStatusCode.OK;
    response.Result = result;
    return Results.Ok(response);
});

// Creates new book or adds another already existing copy with the same name and author.
app.MapPost("/api/book/", async (IValidator<BookCreateDTO> validator, IMapper mapper,
    IBookRepository repo,[FromBody] BookCreateDTO book_c_dto) =>
{
    APIResponse response = new APIResponse();
    ValidationResult validationResult = await validator.ValidateAsync(book_c_dto);

    if (!validationResult.IsValid) return Results.BadRequest(response);

    var book = mapper.Map<Book>(book_c_dto);
    var result = await repo.CreateBook(book);

    var bookDto = mapper.Map<BookDTO>(book);

    response.Result = bookDto;
    response.IsSuccess = true;
    response.StatusCode = HttpStatusCode.Created;
    return Results.Ok(response);
}).WithName("CreateBook").Accepts<BookCreateDTO>("application/json").Produces<APIResponse>(201)
    .Produces(400);

app.MapPut("/api/book/",
    async (IMapper mapper, IValidator<BookUpdateDTO> validator,IBookRepository repo,
        [FromBody] BookUpdateDTO book_u_dto) =>
{
    var response = new APIResponse(){IsSuccess = false, StatusCode = HttpStatusCode.BadRequest};
    ValidationResult validationResult = await validator.ValidateAsync(book_u_dto);

    if (!validationResult.IsValid)
    {
        response.ErrorMessages.Add(validationResult.Errors.FirstOrDefault().ToString());
    }

    var book = mapper.Map<Book>(book_u_dto);
    var result = await repo.UpdateBook(book);
    
    response.Result = result;
    response.IsSuccess = true;
    response.StatusCode = HttpStatusCode.NoContent;
    return Results.Ok(response);
}).WithName("UpdateBook").Accepts<BookUpdateDTO>("application/json")
    .Produces<APIResponse>(200)
    .Produces(400);

app.Run();