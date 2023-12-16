using BloggWebAPI.Controllers;
using BloggWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace BloggWebAPI_Test
{
    [TestClass]
    public class BloggControllerTest
    {
        private ApplicationDbContext _context;
        private BloggController _controller;

        [TestInitialize]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _context = new ApplicationDbContext(options);

            _context.Blogger.Add(new Blogg { BloggId = 1, Tittel = "Test Blogg", Beskrivelse = "Beskrivelse for Test Blogg", EierId = "TestUser1" });
            _context.Blogger.Add(new Blogg { BloggId = 2, Tittel = "Another Blogg", Beskrivelse = "Beskrivelse for Another Blogg", EierId = "TestUser2" });
            _context.SaveChanges();

            _controller = new BloggController(_context);
        }

        [TestMethod]
        public async Task GetBlogg_ValidId_ReturnsBlogg()
        {
            var result = await _controller.GetBlogg(1);

            Assert.IsNotNull(result.Value);
            Assert.AreEqual("Test Blogg", result.Value.Tittel);
        }

        [TestMethod]
        public async Task GetBlogg_InvalidId_ReturnsNotFound()
        {
            var result = await _controller.GetBlogg(99);

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        
        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
        }
    }
}
