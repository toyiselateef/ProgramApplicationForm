namespace ProgramApplicationForm.Api.Models;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public T Data { get; set; } = default!;
    public string Message { get; set; } = string.Empty;

    public static ApiResponse<T> SuccessResponse(T data, string message = "Request successful") => new ApiResponse<T> { Success = true, Data = data, Message = message };
    public static ApiResponse<object> ErrorResponse(string errorMessage) => new ApiResponse<object> { Success = false, Message = errorMessage };
}