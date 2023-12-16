using Microsoft.AspNetCore.Mvc;
using BloggWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BloggWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class KommentarController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public KommentarController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Kommentar
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Kommentar>>> GetKommentarer()
    {
        return await _context.Kommentarer.ToListAsync();
    }

    // GET: api/Kommentar/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Kommentar>> GetKommentar(int id)
    {
        var kommentar = await _context.Kommentarer.FindAsync(id);

        if (kommentar == null)
        {
            return NotFound();
        }

        return kommentar;
    }

    // POST: api/Kommentar
    [HttpPost]
    public async Task<ActionResult<Kommentar>> PostKommentar(Kommentar kommentar)
    {
        _context.Kommentarer.Add(kommentar);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetKommentar", new { id = kommentar.KommentarId }, kommentar);
    }

    // DELETE: api/Kommentar/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteKommentar(int id)
    {
        var kommentar = await _context.Kommentarer.FindAsync(id);
        if (kommentar == null)
        {
            return NotFound();
        }

        _context.Kommentarer.Remove(kommentar);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // GET: api/Kommentar/post/5
    [HttpGet("post/{postId}")]
    public async Task<ActionResult<IEnumerable<Kommentar>>> GetKommentarerForPost(int postId)
    {
        var kommentarer = await _context.Kommentarer
            .Where(k => k.PostId == postId)
            .ToListAsync();

        return kommentarer;
    }

}