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

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Get(string username) {
        var user = await _context.Users
            .Include(u => u.LoginCredentials)
            .Where(u => u.LoginCredentials.Username == username)
            .FirstOrDefaultAsync();

        if (user is null)
            return NotFound();
        
        return Ok(user);
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
