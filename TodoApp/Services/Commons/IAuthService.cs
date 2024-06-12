using TodoApp.Models.Dtos;

namespace TodoApp.Services.Commons;

public interface IAuthService
{
    Task<LoginResponseDto> Login(LoginRequestDto dto);
    Task<bool> Register(RegisterRequestDto dto);
}
