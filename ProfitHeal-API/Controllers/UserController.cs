using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Profit; 

[ApiController]
[Route("api/[controller]")]
public class UserController : Controller {
    private readonly ProfitHealContext _context;
    
    public UserController(ProfitHealContext context) {
        _context = context;
    }
    
    [Authorize]
    [HttpGet]
    public string Check() {
        return "Hello!";
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    [Route("admin")]
    public string CheckAdmin() {
        return "Hello, admin!";
    }


    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] User user) {
        var found = await _context.Users
            .Include(u => u.LoginCredentials)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (found is null)
            return NotFound("User with such id is not found");

        if (found.LoginCredentials != user.LoginCredentials)
            return Forbid("Invalid login credentials");

        found.Name = user.Name;
        found.Surname = user.Surname;
        found.Email = user.Email;
        found.LoginCredentials.Password = user.LoginCredentials.Password;
        found.Roles = user.Roles;
        found.Birthday = user.Birthday;
        
        await _context.SaveChangesAsync();
        return Ok("User has been updated successfully");
    }
}
