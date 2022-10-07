using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProfitHeal_API.Data;
using ProfitHeal_API.Models;
using ProfitHeal_API.Models.ReportModels;

namespace ProfitHeal_API.Controllers.ReportControllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ReportController : Controller {
    private readonly ProfitHealContext _context;

    public ReportController(ProfitHealContext context) {
        _context = context;
    }
    
    [Authorize(Roles="Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAll(string? username, DateTime? date) {
        var reports = await _context.Reports
            .Include(r => r.User)
            .ThenInclude(u => u.LoginCredentials)
            .Include(r => r.Symptom)
            .Where(r => username == null || r.User.LoginCredentials.Username == username)
            .Where(r => date == null || r.Date.Date == date.Value.Date)
            .ToListAsync();
        
        return Ok(reports);
    }

    [HttpPost]
    public async Task<IActionResult> Add(Report report) {
        var found = await _context.Reports.Include(r => r.User)
            .FirstOrDefaultAsync(r => r.User.LoginCredentials.Username == report.User.LoginCredentials.Username);
        if (found is not null)
            return Forbid("You have already sent a report today.");

        await _context.Reports.AddAsync(report);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Remove(int id) {
        var found = await _context.Reports.FirstOrDefaultAsync(r => r.Id == id);
        if (found is null)
            return NotFound();

        _context.Reports.Remove(found);
        await _context.SaveChangesAsync();
        return Ok();
    }
}