using Microsoft.AspNetCore.Mvc;
using BloggWebAPI.Models;
using BloggWebAPI.Models.Services;
using Microsoft.EntityFrameworkCore;

namespace BloggWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly PostService _postService;

        public PostController(PostService postService)
        {
            _postService = postService;
        }

        // GET: api/Post
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts(int pageNumber = 1, int pageSize = 10)
        {
            var posts = await _postService.GetPostsAsync(pageNumber, pageSize);
            return Ok(posts);
        }

        // GET: api/Post/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return post;
        }

        // PUT: api/Post/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost(int id, Post post)
        {
            if (id != post.PostId)
            {
                return BadRequest();
            }
            try
            {
                await _postService.UpdatePostAsync(post);
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_postService.PostExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // POST: api/Post
        [HttpPost]
        public async Task<ActionResult<Post>> PostPost(Post post)
        {
            try
            {
                await _postService.CreatePostAsync(post);
                return CreatedAtAction("GetPost", new { id = post.PostId }, post);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "En feil oppstod under lagring av innlegget.");
            }
        }

        // DELETE: api/Post/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (!_postService.PostExists(id))
            {
                return NotFound();
            }

            await _postService.DeletePostAsync(id);
            return NoContent();
        }

        // GET: api/Post/blog/5
        [HttpGet("blog/{blogId}")]
        public async Task<ActionResult<IEnumerable<Post>>> GetPostsByBlog(int blogId)
        {
            var posts = await _postService.GetPostsByBlogIdAsync(blogId);
            if (posts == null || !posts.Any())
            {
                return NotFound();
            }
            return Ok(posts);
        }

    }
}
