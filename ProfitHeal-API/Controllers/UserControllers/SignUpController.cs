using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProfitHeal_API.Data;
using ProfitHeal_API.Models.UserModels;

namespace ProfitHeal_API.Controllers.UserControllers;

[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
public class SignUpController : Controller {
    private readonly ProfitHealContext _context;

    public SignUpController(ProfitHealContext context) {
        _context = context;
    }
    [HttpPost]
    public async Task<IActionResult> SignUp([FromBody] UserInfo userInfo) {
        var foundUser = await _context.Users
            .Include(u => u.LoginCredentials)
            .FirstOrDefaultAsync(u => u.LoginCredentials.Username == userInfo.Username);
        
        if (foundUser is not null)
            return Conflict();

        var user = new User() {
            Name = userInfo.Name,
            Surname = userInfo.Surname,
            Email = userInfo.Email,
            LoginCredentials = new LoginCredentials() {
                Username = userInfo.Username,
                Password = userInfo.Password
            },
            Roles = await _context.Roles.Where(r => userInfo.Roles.Contains(r.Name)).ToListAsync(),
        };
        
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return Ok();
    }
}
