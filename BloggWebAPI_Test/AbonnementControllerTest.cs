using Microsoft.EntityFrameworkCore;
using BloggWebAPI.Controllers;
using BloggWebAPI.Models;


namespace BloggWebAPI_Test
{
    [TestClass]
    public class AbonnementControllerTest
    {
        private ApplicationDbContext _context;
        private AbonnementController _controller;

        [TestInitialize]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _context = new ApplicationDbContext(options);

            _context.Abonnementer.AddRange(
                new Abonnement { AbonnementId = 1, FollowerId = "User1", FollowingId = "User2" }
            );
            _context.SaveChanges();

            _controller = new AbonnementController(_context);
        }

        [TestMethod]
        public async Task GetAbonnementer_ReturnsAllAbonnementer()
        {
            var result = await _controller.GetAbonnementer();

            // Assert
            Assert.IsNotNull(result.Value);
            var abonnementer = result.Value as List<Abonnement>;
            Assert.AreEqual(1, abonnementer.Count);
        }

    }
}