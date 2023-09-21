using System.Net;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using LibSoft_API.Services;
using LibSoft_Models;
using LibSoft_Models.API_Model_Tools;
using Microsoft.AspNetCore.Mvc;

namespace LibSoft_API.Endpoints;

public static class BookEndpoint
{
    public static void AddBookEndspoints(this IEndpointRouteBuilder app)
    {
// Gets all books.
        app.MapGet("/api/book/",
                async (IBookRepository repo) =>
                {
                    try
                    {
                        var response = new APIResponse
                        {
                            Result = await repo.GetAllBooks(),
                            IsSuccess = true,
                            StatusCode = HttpStatusCode.OK
                        };
                        return Results.Ok(response);
                    }
                    catch (Exception e)
                    {
                        return Results.BadRequest(e);
                    }
                }).WithName("GetAllBooks")
            .Produces<APIResponse>(200);

// Gets book by id.
        app.MapGet("/api/book/{id:int}",
                async (IBookRepository repo, int id) =>
                {
                    try
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
                    }
                    catch (Exception e)
                    {
                        return Results.BadRequest(e);
                    }
                }).WithName("GetBookById")
            .Produces<APIResponse>(200)
            .Produces(400);

// Creates new book or adds another already existing copy with the same name and author.
        app.MapPost("/api/book/",
                async (IValidator<BookCreateDTO> validator, IMapper mapper,
                    IBookRepository repo, [FromBody] BookCreateDTO book_c_dto) =>
                {
                    try
                    {
                        APIResponse response = new APIResponse();
                        ValidationResult validationResult = await validator.ValidateAsync(book_c_dto);

                        if (!validationResult.IsValid) return Results.BadRequest(response);

                        var book = mapper.Map<Book>(book_c_dto);
                        var result = await repo.CreateBook(book);

                        var bookDto = mapper.Map<BookDTO>(result);

                        response.Result = bookDto;
                        response.IsSuccess = true;
                        response.StatusCode = HttpStatusCode.Created;
                        return Results.Ok(response);
                    }
                    catch (Exception e)
                    {
                        return Results.BadRequest(e);
                    }
                }).WithName("CreateBook")
            .Accepts<BookCreateDTO>("application/json")
            .Produces<APIResponse>(201)
            .Produces(400);

        app.MapPut("/api/book/",
                async (IMapper mapper, IValidator<BookUpdateDTO> validator, IBookRepository repo,
                    [FromBody] BookUpdateDTO book_u_dto) =>
                {
                    try
                    {
                        var response = new APIResponse() {IsSuccess = false, StatusCode = HttpStatusCode.BadRequest};
                        ValidationResult validationResult = await validator.ValidateAsync(book_u_dto);

                        if (!validationResult.IsValid)
                        {
                            response.ErrorMessages.Add(validationResult.Errors.FirstOrDefault().ToString());
                            return Results.BadRequest(response);
                        }

                        var book = mapper.Map<Book>(book_u_dto);
                        var result = await repo.UpdateBook(book);

                        response.Result = result;
                        response.IsSuccess = true;
                        response.StatusCode = HttpStatusCode.NoContent;
                        return Results.Ok(response);
                    }
                    catch (Exception e)
                    {
                        return Results.BadRequest(e);
                    }
                }).WithName("UpdateBook").Accepts<BookUpdateDTO>("application/json")
            .Produces<APIResponse>(200)
            .Produces(500);

        app.MapDelete("/api/book/{id:int}",
                async (IBookRepository repo, int id) =>
                {
                    try
                    {
                        var response = new APIResponse() {IsSuccess = false, StatusCode = HttpStatusCode.BadRequest};

                        var result = await repo.DeleteBook(id);
                        if (result == null)
                        {
                            response.ErrorMessages.Add("Invalid ID");
                            response.StatusCode = HttpStatusCode.NotFound;
                            return Results.NotFound(response);
                        }

                        response.IsSuccess = true;
                        response.StatusCode = HttpStatusCode.OK;
                        response.Result = result;
                        return Results.Ok(response);
                    }
                    catch (Exception e)
                    {
                        return Results.BadRequest(e);
                    }
                }).WithName("DeleteBook")
            .Produces<APIResponse>(200)
            .Produces(400);

        app.MapGet("/api/book/author/{auth}",
                async (IBookRepository repo, IMapper mapper, string auth) =>
                {
                    try
                    {
                        var response = new APIResponse() {IsSuccess = false, StatusCode = HttpStatusCode.BadRequest};

                        var result = await repo.SearchByAuthor(auth);

                        var bookDtos = mapper.Map<IEnumerable<BookDTO>>(result);

                        if (result.Any())
                        {
                            response.Result = bookDtos;
                            response.IsSuccess = true;
                            response.StatusCode = HttpStatusCode.OK;
                            return Results.Ok(response);
                        }

                        response.StatusCode = HttpStatusCode.NotFound;
                        response.Result = result;
                        return Results.NotFound(response);
                    }
                    catch (Exception e)
                    {
                        return Results.BadRequest(e);
                    }
                }).WithName("GetBooksByAuthor")
            .Produces<APIResponse>(200)
            .Produces(400);

        app.MapGet("/api/book/genre/{genre}",
                async (IBookRepository repo, IMapper mapper, string genre) =>
                {
                    try
                    {
                        var response = new APIResponse() {IsSuccess = false, StatusCode = HttpStatusCode.BadRequest};

                        var result = await repo.SearchByGenre(genre);

                        var booksDtos = mapper.Map<IEnumerable<BookDTO>>(result);

                        if (result.Any())
                        {
                            response.Result = booksDtos;
                            response.IsSuccess = true;
                            response.StatusCode = HttpStatusCode.OK;
                            return Results.Ok(response);
                        }

                        response.StatusCode = HttpStatusCode.NotFound;
                        response.Result = result;
                        return Results.NotFound(response);
                    }
                    catch (Exception e)
                    {
                        return Results.BadRequest(e);
                    }
                }).WithName("GetBooksByGenre")
            .Produces<APIResponse>(200)
            .Produces(400);
    }
}