using BloggWebAPI.Models;
using Microsoft.AspNetCore.Identity;

public class BlogService
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public BlogService(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task CreateBlogForUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        var blogTitle = (user != null) ? $"{user.UserName} sin Blogg" : "Min Blogg";

        var blogg = new Blogg
        {
            Tittel = blogTitle,
            Beskrivelse = "Beskrivelse av min blogg",
            EierId = userId
        };

        _context.Blogger.Add(blogg);
        await _context.SaveChangesAsync();
    }
}