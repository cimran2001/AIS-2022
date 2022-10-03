using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers; 

[AllowAnonymous]
[ApiController]
[Route("[controller]")]
public class SignUpController : Controller {
    [HttpPost]
    public IActionResult SignUp([FromBody] User user) {
        var foundUser = UserController.Users.FirstOrDefault(u => u.Username == user.Username);
        if (foundUser is not null)
            return Conflict();
        
        UserController.Users.Add(user);
        return Ok();
    }
}