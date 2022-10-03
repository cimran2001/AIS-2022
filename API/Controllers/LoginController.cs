using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers; 

[AllowAnonymous]
[ApiController]
[Route("[controller]")]
public class LoginController : Controller {
    public List<User> Users = new();

    private IConfiguration _configuration;
    
    public LoginController(IConfiguration configuration) {
        _configuration = configuration;
    }
    
    [HttpPost]
    public IActionResult Login([FromBody] User user) {
        if (UserController.Users.FirstOrDefault(u =>
                u.Username == user.Username && u.Password == user.Password) is null)
            return Unauthorized("Not a correct password");
        
        var claims = new List<Claim> {new Claim(ClaimTypes.Name, user.Username) };
        var jwt = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)), // время действия 2 минуты
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])), 
                SecurityAlgorithms.HmacSha256)
            );
            
        return Ok(new JwtSecurityTokenHandler().WriteToken(jwt));
    }
}