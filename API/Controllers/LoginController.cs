using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Extensions;

namespace API.Controllers; 

[AllowAnonymous]
[ApiController]
[Route("[controller]")]
public class LoginController : Controller {
    public List<User> Users = new();

    private readonly IConfiguration _configuration;
    private readonly AISContext _context;
    
    public LoginController(IConfiguration configuration, AISContext context) {
        _configuration = configuration;
        _context = context;
    }
    
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] UserLogin loginData) {
        var user = await _context.Users.Include(u => u.Roles).FirstOrDefaultAsync(u =>
            u.Username == loginData.Username && u.Password == loginData.Password);
        if (user is null)
            return Unauthorized("Not a correct password");
        
        var claims = new List<Claim> {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Surname, user.Surname),
            new Claim(ClaimTypes.Email, user.Email),
        };

        foreach (var role in user.Roles)
            claims.Add(new Claim(ClaimTypes.Role, role.Name));

        var jwt = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])), 
                SecurityAlgorithms.HmacSha256)
            );
        
        return Ok(new JwtSecurityTokenHandler().WriteToken(jwt));
    }
}
