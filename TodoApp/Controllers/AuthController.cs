using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TodoApp.Models.Dtos;
using TodoApp.Services.Commons;

namespace TodoApp.Controllers;

[Route("api/[controller]")]
[ApiController]

public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AuthController(IAuthService authService, RoleManager<IdentityRole> roleManager)
    {
        _authService = authService;
        _roleManager = roleManager;
    }


    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.Login(loginDto);

        if (result.StatusCode == HttpStatusCode.NotFound)
            return NotFound("User Tapilmadi");
        if (result.StatusCode == HttpStatusCode.Unauthorized)
            return Unauthorized("Sifre yanlisdir");

        return Ok(result);
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerDto)
    {




        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.Register(registerDto);

        if (!result)
            return BadRequest("Qeydiyyat tamamlanmadi");
        return Ok("Qeydiyyat tamamlandi");
    }


    [HttpPost("CreateRole")]
    public async Task<IActionResult> CreateRoles()
    {
        await _roleManager.CreateAsync(new IdentityRole("Admin"));
        await _roleManager.CreateAsync(new IdentityRole("User"));
        await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
        return Ok("Roles Created");
    }

}
