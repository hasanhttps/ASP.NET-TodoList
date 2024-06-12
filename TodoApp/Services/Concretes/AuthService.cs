using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Security.Claims;
using TodoApp.Contexts;
using TodoApp.Models.Dtos;
using TodoApp.Models.Entities.Concretes;
using TodoApp.Services.Commons;

namespace TodoApp.Services.Concretes;

public class AuthService : IAuthService
{
    private readonly ITokenService _tokenService;
    private readonly AppDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AuthService(ITokenService tokenService, AppDbContext context, UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _tokenService = tokenService;
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<LoginResponseDto> Login(LoginRequestDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user is null)
            return new LoginResponseDto() { StatusCode = HttpStatusCode.NotFound };

        var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
        if (!result.Succeeded)
            return new LoginResponseDto() { StatusCode = HttpStatusCode.Unauthorized };

        var tokenRequestDto = new TokenRequestDto
        {
            Email = dto.Email,
            Id = user.Id,
            UserName = user.UserName,
            Roles = await _userManager.GetRolesAsync(user),
            Claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, dto.Email),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, string.Join(",", await _userManager.GetRolesAsync(user)))
            }
        };


        var accessToken = _tokenService.GenerateAccessToken(tokenRequestDto);

        return new LoginResponseDto
        {
            AccessToken = accessToken,
            StatusCode = HttpStatusCode.OK
        };
    }

    public async Task<bool> Register(RegisterRequestDto dto)
    {
        var existingUser = await _userManager.FindByEmailAsync(dto.Email);
        if (existingUser is not null)
            return false;

        var newUser = new User()
        {
            Email = dto.Email,
            UserName = dto.UserName,
            Name = dto.FirstName,
            Surname = dto.LastName,
        };

        var result = await _userManager.CreateAsync(newUser, dto.Password);
        //await _userManager.AddToRoleAsync(newUser, dto.Role);
        return result.Succeeded;
    }
}
