

namespace BloggWebAPI.Models
{
    public class Blogg
    {
        public Blogg()
        {
            Poster = new List<Post>();
        }

        public int BloggId { get; set; }
        public string Tittel { get; set; }
        public string Beskrivelse { get; set; }




        public string EierId { get; set; }

        public virtual ICollection<Post> Poster { get; set; }

       
    }
}