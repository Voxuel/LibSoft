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

    public async Task<IActionResult> BookIndex()
    {
        var books = new List<BookDTO>();
        var responseDto = await _bookService.GetAll<ResponseDTO>();
        if (responseDto != null && responseDto.IsSuccess)
        {
            books = JsonConvert.DeserializeObject<List<BookDTO>>(Convert.ToString(responseDto.Result));
        }

        return View(books);
    }
}