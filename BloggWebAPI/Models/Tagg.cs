namespace BloggWebAPI.Models
{
    public class Tagg
    {
        public int TaggId { get; set; }
        public string Navn { get; set; }

        public int PostId { get; set; }
        public virtual Post Post { get; set; }
    }

}
