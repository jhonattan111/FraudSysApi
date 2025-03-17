namespace FraudSysApi.Models
{
    public class ApiResponse<T>
    {
        public int StatusCode { get; }
        public T? Data { get; }
        public string? Message { get; }

        private ApiResponse(int statusCode, T? data, string? message)
        {
            StatusCode = statusCode;
            Data = data;
            Message = message;
        }

        public static ApiResponse<T> Success(T data, int statusCode = StatusCodes.Status200OK) =>
            new(statusCode, data, null);

        public static ApiResponse<T> Error(string message, int statusCode) =>
            new(statusCode, default, message);
    }
}
