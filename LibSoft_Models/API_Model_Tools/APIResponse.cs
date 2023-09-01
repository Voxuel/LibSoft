using System.Net;

namespace LibSoft_Models.API_Model_Tools;

public class APIResponse
{
    public APIResponse()
    {
        ErrorMessages = new List<string>();
    }

    public bool IsSuccess { get; set; }
    public object Result { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public List<string> ErrorMessages { get; set; }
}