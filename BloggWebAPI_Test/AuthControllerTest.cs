using Moq;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using BloggWebAPI.Controllers;
using Microsoft.EntityFrameworkCore;

namespace BloggWebAPI_Test
{
    [TestClass]
    public class AuthControllerTest
    {
        private Mock<UserManager<IdentityUser>> _mockUserManager;
        private Mock<IConfiguration> _mockConfiguration;
        private AuthController _controller;
        private BlogService _blogService;
        private Mock<ApplicationDbContext> _mockDbContext;

        [TestInitialize]
        public void SetUp()
        {
            var userStoreMock = new Mock<IUserStore<IdentityUser>>();
            _mockUserManager = new Mock<UserManager<IdentityUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);

            _mockConfiguration = new Mock<IConfiguration>();
            _mockConfiguration.Setup(c => c["JwtOptions:Key"]).Returns("your_jwt_secret_key_here");

            var options = new DbContextOptions<ApplicationDbContext>();
            _mockDbContext = new Mock<ApplicationDbContext>(options);

            _blogService = new BlogService(_mockDbContext.Object, _mockUserManager.Object);

            _controller = new AuthController(_mockUserManager.Object, _mockConfiguration.Object, _blogService);
        }

        [TestMethod]
        public async Task Register_UserSucceeds_ReturnsOk()
        {
            _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

        }
    }
}