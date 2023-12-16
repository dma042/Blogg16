

namespace BloggWebAPI.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public string Tittel { get; set; }
        public string Innhold { get; set; }
        public DateTime Opprettet { get; set; } = DateTime.UtcNow;

        public int BloggId { get; set; }

    }
}