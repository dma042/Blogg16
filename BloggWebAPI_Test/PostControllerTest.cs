using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BloggWebAPI.Models;
using BloggWebAPI.Controllers;
using BloggWebAPI.Models.Services;

namespace BloggWebAPI_Test
{
    [TestClass]
    public class PostControllerTest
    {
        private ApplicationDbContext _context;
        private PostController _controller;

        [TestInitialize]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _context = new ApplicationDbContext(options);

            _context.Poster.Add(new Post { PostId = 1, Tittel = "Test Post 1", Innhold = "Content 1" });
            _context.Poster.Add(new Post { PostId = 2, Tittel = "Test Post 2", Innhold = "Content 2" });
            _context.SaveChanges();

            var postService = new PostService(_context);
            _controller = new PostController(postService);
        }

        [TestMethod]
        public async Task GetPosts_ReturnsAllPosts()
        {
            // Act
            var result = await _controller.GetPosts();

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var posts = okResult.Value as IEnumerable<Post>;
            Assert.IsNotNull(posts);
            Assert.AreEqual(2, posts.Count());
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}