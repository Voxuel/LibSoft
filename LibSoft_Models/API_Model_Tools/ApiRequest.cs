using System.ComponentModel.DataAnnotations;
using LibSoft_Models;
using LibSoft_Web;

namespace LibSoft_Models.API_Model_Tools;

public class ApiRequest
{
    public StaticDetails.ApiType ApiType { get; set; }
    public string Url { get; set; }
    public object Data { get; set; }
}