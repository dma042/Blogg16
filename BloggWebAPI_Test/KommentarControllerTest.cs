using BloggWebAPI.Controllers;
using BloggWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace BloggWebAPI_Test
{
    [TestClass]
    public class KommentarControllerTest
    {
        private ApplicationDbContext _context;
        private KommentarController _controller;

        [TestInitialize]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _context = new ApplicationDbContext(options);

            _context.Kommentarer.Add(new Kommentar { KommentarId = 1, Innhold = "Test kommentar 1", PostId = 1, ForfatterId = "User1" });
            _context.Kommentarer.Add(new Kommentar { KommentarId = 2, Innhold = "Test kommentar 2", PostId = 1, ForfatterId = "User2" });
            _context.SaveChanges();

            _controller = new KommentarController(_context);
        }


        [TestMethod]
        public async Task GetKommentar_ValidId_ReturnsKommentar()
        {
            var result = await _controller.GetKommentar(1);

            Assert.IsNotNull(result.Value);
            Assert.AreEqual("Test kommentar 1", result.Value.Innhold);
        }

        [TestMethod]
        public async Task GetKommentar_InvalidId_ReturnsNotFound()
        {
            var result = await _controller.GetKommentar(99);

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
        }
    }
}
