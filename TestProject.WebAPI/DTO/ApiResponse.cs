using System.Diagnostics.CodeAnalysis;

namespace TestProject.WebAPI.DTO
{
    /// <summary>
    /// ApiResponse
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [ExcludeFromCodeCoverage]    
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public int StatusCode { get; set; }
        public string Error { get; set; }
        
    }
}
