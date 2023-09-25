using LibSoft_Models;
using LibSoft_Models.API_Model_Tools;

namespace LibSoft_Web.Services;

public class BookService : BaseService , IBookService
{
    private readonly IHttpClientFactory _client;

    public BookService(IHttpClientFactory client, IConfiguration configuration) : base(client)
    {
        _client = client;
    }
    
    public async Task<T> GetAll<T>()
    {
        return await SendAsync<T>(new ApiRequest()
        {
            ApiType = StaticDetails.ApiType.GET,
            Url = StaticDetails.BookApiBase + "/api/book/",
        });
    }

    public async Task<T> GetById<T>(int id)
    {
        return await SendAsync<T>(new ApiRequest()
        {
            ApiType = StaticDetails.ApiType.GET,
            Url = StaticDetails.BookApiBase + $"/api/book/{id}"
        });
    }

    public async Task<T> UpdateAsync<T>(BookDTO bookDto)
    {
        return await SendAsync<T>(new ApiRequest()
        {
            ApiType = StaticDetails.ApiType.PUT,
            Url = StaticDetails.BookApiBase + $"/api/book/",
            Data = bookDto
        });
    }

    public async Task<T> CreateAsync<T>(BookDTO bookDto)
    {
        return await SendAsync<T>(new ApiRequest()
        {
            ApiType = StaticDetails.ApiType.POST,
            Url = StaticDetails.BookApiBase + $"/api/book/",
            Data = bookDto
        });
    }

    public async Task<T> DeleteAsync<T>(int id)
    {
        return await SendAsync<T>(new ApiRequest()
        {
            ApiType = StaticDetails.ApiType.DELETE,
            Url = StaticDetails.BookApiBase + $"/api/book/{id}"
        });
    }

    public Task<T> SearchByAuthor<T>(string queryString)
    {
        return SendAsync<T>(new ApiRequest()
        {
            ApiType = StaticDetails.ApiType.GET,
            Url = StaticDetails.BookApiBase + $"/api/book/author/{queryString}"
        });
    }

    public Task<T> SearchByGenre<T>(string queryString)
    {
        return SendAsync<T>(new ApiRequest()
        {
            ApiType = StaticDetails.ApiType.GET,
            Url = StaticDetails.BookApiBase + $"/api/book/genre/{queryString}"
        });
    }
}