using Microsoft.AspNetCore.Mvc;
using BloggWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BloggWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AbonnementController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AbonnementController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Abonnement
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Abonnement>>> GetAbonnementer()
    {
        return await _context.Abonnementer.ToListAsync();
    }

    // POST: api/Abonnement
    [HttpPost]
    public async Task<ActionResult<Abonnement>> PostAbonnement(Abonnement abonnement)
    {
        _context.Abonnementer.Add(abonnement);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetAbonnement", new { id = abonnement.AbonnementId }, abonnement);
    }

    // DELETE: api/Abonnement/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAbonnement(int id)
    {
        var abonnement = await _context.Abonnementer.FindAsync(id);
        if (abonnement == null)
        {
            return NotFound();
        }

        _context.Abonnementer.Remove(abonnement);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}