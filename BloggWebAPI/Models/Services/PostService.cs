using Microsoft.EntityFrameworkCore;

namespace BloggWebAPI.Models.Services
{
    public class PostService
    {
        private readonly ApplicationDbContext _context;

        public PostService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Post>> GetPostsAsync(int pageNumber, int pageSize)
        {
            return await _context.Poster
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task CreatePostAsync(Post post)
        {
            if (string.IsNullOrEmpty(post.Tittel) || string.IsNullOrEmpty(post.Innhold))
            {
                throw new InvalidOperationException("Posten må ha en tittel og innhold.");
            }

            _context.Poster.Add(post);
            await _context.SaveChangesAsync();
        }

        public async Task<Post> GetPostByIdAsync(int id)
        {
            return await _context.Poster
                .FirstOrDefaultAsync(p => p.PostId == id);
        }

        public async Task UpdatePostAsync(Post post)
        {
            _context.Entry(post).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeletePostAsync(int id)
        {
            var post = await _context.Poster.FindAsync(id);
            if (post != null)
            {
                _context.Poster.Remove(post);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Post>> GetPostsByBlogIdAsync(int blogId)
        {
            return await _context.Poster
                .Where(p => p.BloggId == blogId)
                .ToListAsync();
        }


        public bool PostExists(int id)
        {
            return _context.Poster.Any(e => e.PostId == id);
        }
    }
}
