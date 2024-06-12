using System.Net;

namespace TodoApp.Models.Dtos;

public class LoginResponseDto
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public HttpStatusCode? StatusCode { get; set; }
}
