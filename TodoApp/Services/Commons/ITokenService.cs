using TodoApp.Models.Dtos;
using TodoApp.Models.Entities.Concretes;

namespace TodoApp.Services.Commons;

public interface ITokenService
{
    string GenerateAccessToken(TokenRequestDto dto);
    string GenerateRefreshToken();
}
