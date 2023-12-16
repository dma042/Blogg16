using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace BloggWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SearchController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public SearchController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Search?query=example
    [HttpGet]
    public async Task<IActionResult> Search(string query)
    {
        var posts = await _context.Poster
            .Where(p => p.Tittel.Contains(query) || p.Innhold.Contains(query))
            .ToListAsync();

        var blogger = await _context.Blogger
            .Where(b => b.Tittel.Contains(query) || b.Beskrivelse.Contains(query))
            .ToListAsync();

        var kommentarer = await _context.Kommentarer
            .Where(k => k.Innhold.Contains(query))
            .ToListAsync();

        var users = await _context.Users
            .Where(u => u.UserName.Contains(query))
            .ToListAsync();

        var result = new
        {
            Poster = posts,
            Blogger = blogger,
            Kommentarer = kommentarer,
            Users = users
        };

        return Ok(result);
    }

    [HttpGet("users")]
    public async Task<ActionResult<IEnumerable<IdentityUser>>> SearchUsers(string query)
    {
        var users = await _context.Users
            .Where(u => u.UserName.Contains(query))
            .ToListAsync();

        return users;
    }

}