namespace LibSoft_Models.API_Model_Tools;

public class ResponseDTO
{
    public bool IsSuccess { get; set; } = true;
    public object Result { get; set; }
    public string ErrorMessage { get; set; } = "";
    public List<string> ErrorMessages { get; set; }
}