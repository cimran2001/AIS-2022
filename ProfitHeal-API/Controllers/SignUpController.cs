using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Profit; 

[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
public class SignUpController : Controller {
    private readonly ProfitHealContext _context;

    public SignUpController(ProfitHealContext context) {
        _context = context;
    }
    [HttpPost]
    public async Task<IActionResult> SignUp([FromBody] User user) {
        var foundUser = await _context.Users
            .Include(u => u.LoginCredentials)
            .FirstOrDefaultAsync(u => u.LoginCredentials.Username == user.LoginCredentials.Username);
        
        if (foundUser is not null)
            return Conflict();
        
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return Ok();
    }
}
