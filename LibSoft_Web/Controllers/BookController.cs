using LibSoft_Models;
using LibSoft_Models.API_Model_Tools;
using LibSoft_Web.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LibSoft_Web.Controllers;

public class BookController : Controller
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }

    public async Task<IActionResult> BookIndex(string searchString, IFormCollection form)
    {
        var books = new List<BookDTO>();
        
        var responseDto = await _bookService.GetAll<ResponseDTO>();
        var searchType = form["searchType"].ToString();
        
        if (!string.IsNullOrEmpty(searchString) && searchType.ToLower() == "author")
        {
            responseDto = await _bookService.SearchByAuthor<ResponseDTO>(searchString);
        }
        if (!string.IsNullOrEmpty(searchString) && searchType.ToLower() == "genre")
        {
            responseDto = await _bookService.SearchByGenre<ResponseDTO>(searchString);
        }
        
        if (responseDto != null && responseDto.IsSuccess)
        {
            books = JsonConvert.DeserializeObject<List<BookDTO>>(Convert.ToString(responseDto.Result));
        }

        return View(books);
    }

    public async Task<IActionResult> Details(int id)
    {
        var resDto = await _bookService.GetById<ResponseDTO>(id);
        if (resDto == null || !resDto.IsSuccess) return NotFound();

        BookDTO book_dto_detailed = JsonConvert.DeserializeObject<BookDTO>(Convert.ToString(resDto.Result));
        return View(book_dto_detailed);
    }
    
    
}