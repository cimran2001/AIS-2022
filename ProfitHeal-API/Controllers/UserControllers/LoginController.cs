using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProfitHeal_API.Data;
using ProfitHeal_API.Models.UserModels;

namespace ProfitHeal_API.Controllers.UserControllers; 

[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
public class LoginController : Controller {
    private readonly IConfiguration _configuration;
    private readonly ProfitHealContext _context;
    
    public LoginController(IConfiguration configuration, ProfitHealContext context) {
        _configuration = configuration;
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginCredentials loginCredentials) {
        var user = await _context.Users
            .Include(u => u.Roles)
            .Include(u => u.LoginCredentials)
            .FirstOrDefaultAsync(u =>
                u.LoginCredentials.Username == loginCredentials.Username &&
                u.LoginCredentials.Password == loginCredentials.Password);
        
        if (user is null)
            return Unauthorized("Not a correct password");

        var claims = new List<Claim> {
            new (ClaimTypes.Name, user.Name),
            new (ClaimTypes.Surname, user.Surname),
            new (ClaimTypes.Email, user.Email), 
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
                SecurityAlgorithms.HmacSha512)
            );
        
        return Ok(new JwtSecurityTokenHandler().WriteToken(jwt));
    }
}
