using BloggWebAPI.Controllers;
using BloggWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace BloggWebAPI_Test
{
    [TestClass]
    public class NotificationControllerTest
    {
        private ApplicationDbContext _context;
        private NotificationController _controller;

        [TestInitialize]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _context = new ApplicationDbContext(options);

            _context.Notifikasjoner.Add(new Notifikasjon { NotifikasjonId = 1, Melding = "Test Notifikasjon 1", ErLest = false });
            _context.Notifikasjoner.Add(new Notifikasjon { NotifikasjonId = 2, Melding = "Test Notifikasjon 2", ErLest = true });
            _context.SaveChanges();

            _controller = new NotificationController(_context);
        }


        [TestMethod]
        public async Task GetNotifikasjon_ValidId_ReturnsNotifikasjon()
        {
            var result = await _controller.GetNotifikasjon(1);

            Assert.IsNotNull(result.Value);
            Assert.AreEqual("Test Notifikasjon 1", result.Value.Melding);
        }

        [TestMethod]
        public async Task GetNotifikasjon_InvalidId_ReturnsNotFound()
        {
            var result = await _controller.GetNotifikasjon(99);

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }


        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
        }
    }
}
