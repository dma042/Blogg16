using Microsoft.AspNetCore.Identity;

namespace BloggBlazorServer.Models
{
    public class Abonnement
    {
        public int AbonnementId { get; set; }

        public string FollowerId { get; set; }
        public string FollowingId { get; set; }

        public virtual IdentityUser Follower { get; set; }
        public virtual IdentityUser Following { get; set; }
    }
}