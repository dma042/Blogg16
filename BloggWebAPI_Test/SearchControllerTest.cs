using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BloggWebAPI.Models;
using BloggWebAPI.Controllers;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace BloggWebAPI_Test
{
    [TestClass]
    public class SearchControllerTest
    {
        private ApplicationDbContext _context;
        private SearchController _controller;

        [TestInitialize]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _context = new ApplicationDbContext(options);

            var testUser = new IdentityUser { Id = "TestId", UserName = "TestUser1" };
            _context.Users.Add(testUser);
            _context.Blogger.Add(new Blogg { BloggId = 1, Tittel = "Test Blogg 1", Beskrivelse = "Description 1", EierId = testUser.Id });

            
            _context.Poster.Add(new Post { PostId = 1, Tittel = "Test Post 1", Innhold = "Content 1", BloggId = 1 });
            _context.Kommentarer.Add(new Kommentar { KommentarId = 1, Innhold = "Comment 1", PostId = 1, ForfatterId = testUser.Id });

            _context.SaveChanges();

            _controller = new SearchController(_context);
        }



        public class SearchResult
        {
            public List<Post> Poster { get; set; }
            public List<Blogg> Blogger { get; set; }
            public List<Kommentar> Kommentarer { get; set; }
            public List<IdentityUser> Users { get; set; }
        }

        [TestMethod]
        public async Task Search_ReturnsCorrectResults()
        {
            var searchQuery = "test";

            var result = await _controller.Search(searchQuery) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            var responseData = result.Value as SearchResult;
            Assert.IsNotNull(responseData, "responseData is null");

            Assert.IsNotNull(responseData.Poster, "Poster list is null");
            Assert.IsNotNull(responseData.Blogger, "Blogger list is null");
            Assert.IsNotNull(responseData.Kommentarer, "Kommentarer list is null");
            Assert.IsNotNull(responseData.Users, "Users list is null");

           
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
