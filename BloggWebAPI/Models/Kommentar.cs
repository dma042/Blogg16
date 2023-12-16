
namespace BloggWebAPI.Models
{
    public class Kommentar
    {
        public int KommentarId { get; set; }
        public string Innhold { get; set; }
        public string ForfatterId { get; set; }

        public int PostId { get; set; }
    }

}
