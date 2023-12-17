using Microsoft.AspNetCore.Mvc;
using BloggWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BloggWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirektemeldingController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DirektemeldingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Direktemelding/meldinger/{userId}
        [HttpGet("meldinger/{userId}")]
        public async Task<ActionResult<IEnumerable<Direktemelding>>> GetMeldingerForBruker(string userId)
        {
            var meldinger = await _context.Direktemeldinger
                .Where(m => m.AvsenderId == userId || m.MottakerId == userId)
                .ToListAsync();

            return meldinger;
        }

        // POST: api/Direktemelding
        [HttpPost]
        public async Task<ActionResult<Direktemelding>> PostDirektemelding(Direktemelding direktemelding)
        {
            _context.Direktemeldinger.Add(direktemelding);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMeldingerForBruker", new { userId = direktemelding.AvsenderId }, direktemelding);
        }
    }
}