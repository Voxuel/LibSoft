using LibSoft_Models;

namespace LibSoft_Web.Services;

public interface IBookService
{
    Task<T> GetAll<T>();
    Task<T> GetById<T>(int id);
    Task<T> UpdateAsync<T>(BookDTO bookDto);
    Task<T> CreateAsync<T>(BookDTO bookDto);
    Task<T> DeleteAsync<T>(int id);
    Task<T> SearchByAuthor<T>(string queryString);
    Task<T> SearchByGenre<T>(string queryString);
}