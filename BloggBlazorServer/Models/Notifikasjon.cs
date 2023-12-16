namespace BloggBlazorServer.Models
{
    public class Notifikasjon
    {
        public int NotifikasjonId { get; set; }
        public string Melding { get; set; }
        public DateTime Opprettet { get; set; }
        public bool ErLest { get; set; }

    }
}