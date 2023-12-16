

namespace BloggBlazorServer.Models
{
    public class Blogg
    {
        public int BloggId { get; set; }
        public string Tittel { get; set; }
        public string Beskrivelse { get; set; }
        public string EierId { get; set; }

        public virtual ICollection<Post> Poster { get; set; }
    }
}