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
}
