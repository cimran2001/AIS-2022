using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProfitHeal_API.Data;
using ProfitHeal_API.Models.UserModels;

namespace ProfitHeal_API.Controllers.UserControllers; 

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/[controller]")]
public class RoleController : Controller {
    private readonly ProfitHealContext _context;

    public RoleController(ProfitHealContext context) {
        _context = context;
    }

    [HttpGet] 
    public IActionResult GetAll() {
        return Ok(_context.Roles.Include(r => r.Users));
    }

    // [HttpPost]
    // public async Task<IActionResult> Add([FromBody] Role role) {
    //     var found = await _context.Roles.FirstOrDefaultAsync(r => r.Name == role.Name);
    //     if (found is not null)
    //         return Conflict("Role with such a name exists");
    //
    //     await _context.Roles.AddAsync(role);
    //     await _context.SaveChangesAsync();
    //     return Ok();
    // }

    // [HttpDelete("{name}")]
    // public async Task<IActionResult> Remove(string name) {
    //     var found = await _context.Roles.FirstOrDefaultAsync(r => r.Name == name);
    //     if (found is null)
    //         return NotFound("Role with such an ID does not exist");
    //
    //     _context.Roles.Remove(found);
    //     await _context.SaveChangesAsync();
    //     return Ok();
    // }
}
