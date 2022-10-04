using API.Data;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers; 

[AllowAnonymous]
[ApiController]
[Route("[controller]")]
public class SignUpController : Controller {
    private readonly AISContext _context;

    public SignUpController(AISContext context) {
        _context = context;
    }
    [HttpPost]
    public async Task<IActionResult> SignUp([FromBody] User user) {
        var foundUser = _context.Users.FirstOrDefault(u => u.Username == user.Username);
        if (foundUser is not null)
            return Conflict();
        
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return Ok();
    }
}
