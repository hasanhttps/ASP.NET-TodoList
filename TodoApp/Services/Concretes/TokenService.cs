using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using TodoApp.Models.Dtos;
using TodoApp.Services.Commons;

namespace TodoApp.Services.Concretes;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateAccessToken(TokenRequestDto dto)
    {
        var jwtAccessToken = new JwtSecurityToken(
            issuer: _configuration["JWT:Issuer"],
            audience: _configuration["JWT:Audience"],
            claims: dto.Claims,
            expires: DateTime.Now.AddMinutes(30),  
            
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!)),               SecurityAlgorithms.HmacSha256Signature)
            );

        return new JwtSecurityTokenHandler().WriteToken(jwtAccessToken);
    }

    public string GenerateRefreshToken()
    {
        return Guid.NewGuid().ToString().ToLower();
    }
}
