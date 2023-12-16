using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BloggWebAPI.Models;

namespace BloggWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BloggController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public BloggController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Blogg
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Blogg>>> GetBlogger()
    {
        return await _context.Blogger.ToListAsync();
    }

    // GET: api/Blogg/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Blogg>> GetBlogg(int id)
    {
        var blogg = await _context.Blogger.FindAsync(id);

        if (blogg == null)
        {
            return NotFound();
        }

        return blogg;
    }

    // GET: api/Blogg/user/5
    [HttpGet("user/{userId}")]
    public async Task<ActionResult<Blogg>> GetBloggByUserId(string userId)
    {
        var blogg = await _context.Blogger.FirstOrDefaultAsync(b => b.EierId == userId);

        if (blogg == null)
        {
            return NotFound();
        }

        return blogg;
    }


    // PUT: api/Blogg/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBlogg(int id, Blogg blogg)
    {
        if (id != blogg.BloggId)
        {
            return BadRequest();
        }

        _context.Entry(blogg).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BloggExists(id))
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

    // POST: api/Blogg
    [HttpPost]
    public async Task<ActionResult<Blogg>> PostBlogg(Blogg blogg)
    {
        _context.Blogger.Add(blogg);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetBlogg), new { id = blogg.BloggId }, blogg);
    }

    // DELETE: api/Blogg/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBlogg(int id)
    {
        var blogg = await _context.Blogger.FindAsync(id);
        if (blogg == null)
        {
            return NotFound();
        }

        _context.Blogger.Remove(blogg);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool BloggExists(int id)
    {
        return _context.Blogger.Any(e => e.BloggId == id);
    }

    // GET: api/Blogg/5/innlegg
    [HttpGet("{id}/innlegg")]
    public async Task<ActionResult<IEnumerable<Post>>> GetInnleggForBlogg(int id)
    {
        var innlegg = await _context.Poster
            .Where(p => p.BloggId == id)
            .ToListAsync();

        if (innlegg == null)
        {
            return NotFound();
        }

        return innlegg;
    }

}
