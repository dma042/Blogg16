using Microsoft.AspNetCore.Mvc;
using BloggWebAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace BloggWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotificationController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public NotificationController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Notification
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Notifikasjon>>> GetNotifikasjoner()
    {
        return await _context.Notifikasjoner.ToListAsync();
    }

    // GET: api/Notification/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Notifikasjon>> GetNotifikasjon(int id)
    {
        var notifikasjon = await _context.Notifikasjoner.FindAsync(id);

        if (notifikasjon == null)
        {
            return NotFound();
        }

        return notifikasjon;
    }

    // POST: api/Notification
    [HttpPost]
    public async Task<ActionResult<Notifikasjon>> PostNotifikasjon(Notifikasjon notifikasjon)
    {
        _context.Notifikasjoner.Add(notifikasjon);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetNotifikasjon", new { id = notifikasjon.NotifikasjonId }, notifikasjon);
    }

    // DELETE: api/Notification/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNotifikasjon(int id)
    {
        var notifikasjon = await _context.Notifikasjoner.FindAsync(id);
        if (notifikasjon == null)
        {
            return NotFound();
        }

        _context.Notifikasjoner.Remove(notifikasjon);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}