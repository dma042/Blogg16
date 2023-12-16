using Microsoft.AspNetCore.Identity;

namespace BloggWebAPI.Models
{
    public class Varsling
    {
        public int VarslingId { get; set; }
        public string UserId { get; set; }
        public string Melding { get; set; }
        public bool ErLest { get; set; }

        public virtual IdentityUser User { get; set; }
    }

}
