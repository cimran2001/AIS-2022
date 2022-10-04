using API.Data;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers; 

[ApiController]
[Route("[controller]")]
public class UserController : Controller {
    private readonly AISContext _context;
    
    public UserController(AISContext context) {
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
}
