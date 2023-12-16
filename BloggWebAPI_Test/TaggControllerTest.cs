using Microsoft.EntityFrameworkCore;
using BloggWebAPI.Models;
using BloggWebAPI.Controllers;


namespace BloggWebAPI_Test
{
    [TestClass]
    public class TaggControllerTest
    {
        private ApplicationDbContext _context;
        private TaggController _controller;

        [TestInitialize]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _context = new ApplicationDbContext(options);

            _context.Tagger.AddRange(
                new Tagg { TaggId = 1, Navn = "Test1" },
                new Tagg { TaggId = 2, Navn = "Test2" });
            _context.SaveChanges();

            _controller = new TaggController(_context);
        }

        [TestMethod]
        public async Task GetTagger_ReturnsAllTagger()
        {
            // Act
            var result = await _controller.GetTagger();

            // Assert
            var tagger = result.Value as List<Tagg>;
            Assert.IsNotNull(tagger);
            Assert.AreEqual(2, tagger.Count);
        }


        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
        }

    }
}