using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProfitHeal_API.Data;
using ProfitHeal_API.Models.UserModels;

namespace ProfitHeal_API.Controllers.UserControllers; 

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController : Controller {
    private readonly ProfitHealContext _context;
    
    public UserController(ProfitHealContext context) {
        _context = context;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> Get(string? roleName) {
        var users = await _context.Users
            .Include(u => u.Roles)
            .Where(u => roleName == null || u.Roles.Any(r => r.Name == roleName))
            .ToListAsync();
        return Ok(users);
    }

    // DOESN'T WORK
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UserInfo userInfo) {
        var found = await _context.Users
            .Include(u => u.Roles)
            .Include(u => u.LoginCredentials)
            .FirstOrDefaultAsync(u => u.Id == id);
        
        if (found is null)
            return NotFound("User with such an id is not found.");

        found.Name = userInfo.Name;
        found.Surname = userInfo.Surname;
        found.Birthday = userInfo.Birthday;
        found.Email = userInfo.Email;

        var loginCredentials =
            await _context.LoginCredentials.FirstAsync(lc => lc.Username == userInfo.Username);
        loginCredentials.Password = userInfo.Password;
        
        found.Roles.Clear();
        found.Roles.AddRange(
            await _context.Roles.Where(r => userInfo.Roles.Contains(r.Name)).ToListAsync());

        await _context.SaveChangesAsync();
        return Ok();
    }
}
