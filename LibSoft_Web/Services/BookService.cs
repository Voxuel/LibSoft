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

    public Task<T> GetById<T>(int id)
    {
        throw new NotImplementedException();
    }

    public Task<T> UpdateAsync<T>(BookDTO bookDto)
    {
        throw new NotImplementedException();
    }

    public Task<T> CreateAsync<T>(BookDTO bookDto)
    {
        throw new NotImplementedException();
    }

    public Task<T> DeleteAsync<T>(int id)
    {
        throw new NotImplementedException();
    }
}