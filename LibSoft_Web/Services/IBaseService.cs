using LibSoft_Models.API_Model_Tools;

namespace LibSoft_Web.Services;

public interface IBaseService : IDisposable
{
    ResponseDTO ResponseModel { get; set; }
    Task<T> SendAsync<T>(ApiRequest request);
}