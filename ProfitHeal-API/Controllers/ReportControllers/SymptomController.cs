using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProfitHeal_API.Data;
using ProfitHeal_API.Models;
using ProfitHeal_API.Models.ReportModels;

namespace ProfitHeal_API.Controllers.ReportControllers;

[ApiController]
[Route("api/[controller]")]
public class SymptomController : Controller {
    private readonly ProfitHealContext _context;

    public SymptomController(ProfitHealContext context) {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll(bool sorted = false) {
        if (sorted is false)
            return Ok(_context.Symptoms);
        return Ok(_context.Symptoms
            .Include(s => s.Category)
            .OrderBy(s => s.Category.Name));
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] SymptomInfo symptomInfo) {
        var category = await _context.SymptomCategories.FirstOrDefaultAsync(sc => sc.Name == symptomInfo.Category);
        if (category is null)
            return NotFound("This category does not exist.");
        

        var symptom = new Symptom() {
            Name = symptomInfo.Name,
            Category = category,
        };
        await _context.Symptoms.AddAsync(symptom);
        await _context.SaveChangesAsync();
        return Ok();
    }
    
    [Authorize(Roles = "Admin")]
    [HttpDelete("{name}")]
    public async Task<IActionResult> Remove(string name) {
        var found = await _context.Symptoms.FirstOrDefaultAsync(s => s.Name == name);
        if (found is null)
            return NotFound();
    
        _context.Symptoms.Remove(found);
        await _context.SaveChangesAsync();
        return Ok();
    }
}
