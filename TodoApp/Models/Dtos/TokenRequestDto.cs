using System.Security.Claims;

namespace TodoApp.Models.Dtos;

public class TokenRequestDto
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public IList<string> Roles { get; set; }
    public List<Claim> Claims { get; set; }
}
