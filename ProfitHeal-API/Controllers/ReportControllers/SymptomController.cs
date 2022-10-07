using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProfitHeal_API.Data;
using ProfitHeal_API.Models;

namespace ProfitHeal_API.Controllers.ReportControllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class SymptomController : Controller {
    private readonly ProfitHealContext _context;

    public SymptomController(ProfitHealContext context) {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll() {
        return Ok(_context.Symptoms);
    }

    // [HttpPost]
    // public async Task<IActionResult> Add([FromBody] Symptom symptom) {
    //     await _context.Symptoms.AddAsync(symptom);
    //     await _context.SaveChangesAsync();
    //     return Ok();
    // }
    //
    // [HttpDelete("{name}")]
    // public async Task<IActionResult> Remove(string name) {
    //     var found = await _context.Symptoms.FirstOrDefaultAsync(s => s.Name == name);
    //     if (found is null)
    //         return NotFound();
    //
    //     _context.Symptoms.Remove(found);
    //     await _context.SaveChangesAsync();
    //     return Ok();
    // }
}
