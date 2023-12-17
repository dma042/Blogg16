using Microsoft.AspNetCore.Mvc;

namespace BloggWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public ImageController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpGet("{fileName}")]
        public IActionResult GetImage(string fileName)
        {
            var imagePath = Path.Combine(_env.ContentRootPath, "uploads", fileName);
            var imageBytes = System.IO.File.ReadAllBytes(imagePath);
            return File(imageBytes, "image/jpeg");
        }
    }
}