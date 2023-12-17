using System;
using Microsoft.AspNetCore.Identity;

namespace BloggBlazorServer.Models
{
    public class Direktemelding
    {
        public int DirektemeldingId { get; set; }
        public string AvsenderId { get; set; }
        public string MottakerId { get; set; } 
        public string Innhold { get; set; }
        public DateTime SendtTidspunkt { get; set; }

        public virtual IdentityUser Avsender { get; set; }
        public virtual IdentityUser Mottaker { get; set; }
    }
}