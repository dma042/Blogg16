using Microsoft.AspNetCore.Identity;

namespace BloggBlazorServer.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public string Tittel { get; set; }
        public string Innhold { get; set; }
        public DateTime Opprettet { get; set; }

        public int BloggId { get; set; }
        public virtual Blogg Blogg { get; set; }

        public virtual ICollection<Kommentar> Kommentarer { get; set; }
        public virtual ICollection<Tagg> Tagger { get; set; }

        public virtual ICollection<IdentityUser> TaggedUsers { get; set; }
    }
}