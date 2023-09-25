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

        var order = form["orderBy"].ToString();
        
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

        return order.ToLower() == "author name"
            ? View(books.OrderBy(b => b.Author))
            : View(books.OrderBy(b => b.Title));
    }

    public async Task<IActionResult> Details(int id)
    {
        var responseDto = await _bookService.GetById<ResponseDTO>(id);
        if (responseDto is not {IsSuccess: true}) return NotFound();

        BookDTO book_dto_detailed = JsonConvert.DeserializeObject<BookDTO>(Convert.ToString(responseDto.Result));
        return View(book_dto_detailed);
    }

    public async Task<IActionResult> Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(BookDTO createDto)
    {
        if (!ModelState.IsValid) return View(createDto);

        var responseDto = await _bookService.CreateAsync<ResponseDTO>(createDto);
        if (responseDto is {IsSuccess: true})
        {
            return RedirectToAction(nameof(BookIndex));
        }

        return View(createDto);
    }
    
    public async Task<IActionResult> Update(int id)
    {
        var responseDto = await _bookService.GetById<ResponseDTO>(id);
        if (responseDto is not {IsSuccess: true}) return NotFound();

        BookDTO dto = JsonConvert.DeserializeObject<BookDTO>(Convert.ToString(responseDto.Result));
        return View(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Update(BookDTO updateDto)
    {
        if (!ModelState.IsValid) return View(updateDto);
        var responseDto = await _bookService.UpdateAsync<ResponseDTO>(updateDto);
        if (responseDto is {IsSuccess: true})
        {
            return RedirectToAction(nameof(BookIndex));
        }

        return View(updateDto);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var responseDto = await _bookService.GetById<ResponseDTO>(id);
        if (responseDto is not {IsSuccess: true}) return NotFound();
        var dto = JsonConvert.DeserializeObject<BookDTO>(Convert.ToString(responseDto.Result));
        return View(dto);
    }
    [HttpPost]
    public async Task<IActionResult> DeleteBook(int id)
    {
        if (!ModelState.IsValid) return NotFound();
        var responseDto = await _bookService.DeleteAsync<ResponseDTO>(id);
        if (responseDto is {IsSuccess: true})
        {
            return RedirectToAction(nameof(BookIndex));
        }

        return NotFound();
    }
}