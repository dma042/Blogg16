using System.Security.Claims;
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
        var currentUserId = GetCurrentUserId();
        if (kommentar.ForfatterId != currentUserId)
        {
            return Forbid();
        }

        _context.Kommentarer.Remove(kommentar);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private string GetCurrentUserId()
    {
       return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
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

    // PUT: api/Kommentar/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutKommentar(int id, Kommentar kommentar)
    {
        if (id != kommentar.KommentarId)
        {
            return BadRequest();
        }
        _context.Entry(kommentar).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!KommentarExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    private bool KommentarExists(int id)
    {
        return _context.Kommentarer.Any(e => e.KommentarId == id);
    }




}