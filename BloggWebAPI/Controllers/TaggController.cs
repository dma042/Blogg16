using Microsoft.AspNetCore.Mvc;
using BloggWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BloggWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TaggController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TaggController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Tagg
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tagg>>> GetTagger()
    {
        return await _context.Tagger.ToListAsync();
    }

    // GET: api/Tagg/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Tagg>> GetTagg(int id)
    {
        var tagg = await _context.Tagger.FindAsync(id);

        if (tagg == null)
        {
            return NotFound();
        }

        return tagg;
    }

    // POST: api/Tagg
    [HttpPost]
    public async Task<ActionResult<Tagg>> PostTagg(Tagg tagg)
    {
        _context.Tagger.Add(tagg);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetTagg", new { id = tagg.TaggId }, tagg);
    }

    // DELETE: api/Tagg/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTagg(int id)
    {
        var tagg = await _context.Tagger.FindAsync(id);
        if (tagg == null)
        {
            return NotFound();
        }

        _context.Tagger.Remove(tagg);
        await _context.SaveChangesAsync();

        return NoContent();
    }

}