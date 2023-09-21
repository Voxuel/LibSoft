using System.Text;
using LibSoft_Models.API_Model_Tools;
using Newtonsoft.Json;

namespace LibSoft_Web.Services;

public class BaseService : IBaseService
{
    public void Dispose()
    {
        GC.SuppressFinalize(true);
    }
    
    public ResponseDTO ResponseModel { get; set; }
    public IHttpClientFactory ClientFactory { get; set; }

    public BaseService(IHttpClientFactory httpClient)
    {
        ClientFactory = httpClient;
        ResponseModel = new ResponseDTO();
    }
    
    public async Task<T> SendAsync<T>(ApiRequest request)
    {
        try
        {
            var client = ClientFactory.CreateClient("LibSoft_API");
            HttpRequestMessage message = new HttpRequestMessage();
            message.Headers.Add("Accept", "application/json");
            message.RequestUri = new Uri(request.Url);
            client.DefaultRequestHeaders.Clear();

            if (request.Data != null)
            {
                message.Content = new StringContent(JsonConvert.SerializeObject(request.Data), Encoding.UTF8,
                    "application/json");
            }

            HttpResponseMessage responseMessage = null;

            switch (request.ApiType)
            {
                case StaticDetails.ApiType.GET:
                    message.Method = HttpMethod.Get;
                    break;
                case StaticDetails.ApiType.PUT:
                    message.Method = HttpMethod.Put;
                    break;
                case StaticDetails.ApiType.POST:
                    message.Method = HttpMethod.Post;
                    break;
                case StaticDetails.ApiType.DELETE: 
                    message.Method = HttpMethod.Delete;
                    break;
            }

            responseMessage = await client.SendAsync(message);

            var content = await responseMessage.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<T>(content);
            return responseDto;

        }
        catch (Exception e)
        {
            var dto = new ResponseDTO()
            {
                ErrorMessage = "Error",
                ErrorMessages = new List<string>() {Convert.ToString(e.Message)},
                IsSuccess = false
            };
            var response = JsonConvert.SerializeObject(dto);
            var resDto = JsonConvert.DeserializeObject<T>(response);
            return resDto;
        }
    }
}