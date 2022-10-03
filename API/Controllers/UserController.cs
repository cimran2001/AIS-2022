using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers; 

[ApiController]
[Route("[controller]")]
public class UserController : Controller {
    public static List<User> Users { get; set; } = new();
    
    [Authorize]
    [HttpGet]
    public string Check() {
        return "Hello!";
    }
}