using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BloggWebAPI.Models;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Blogg> Blogger { get; set; }
    public DbSet<Post> Poster { get; set; }
    public DbSet<Kommentar> Kommentarer { get; set; }
    public DbSet<Tagg> Tagger { get; set; }
    public DbSet<Abonnement> Abonnementer { get; set; }
    public DbSet<Notifikasjon> Notifikasjoner { get; set; }

    public DbSet<Direktemelding> Direktemeldinger { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Abonnement>()
            .HasOne(a => a.Follower)
            .WithMany()
            .HasForeignKey(a => a.FollowerId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Abonnement>()
            .HasOne(a => a.Following)
            .WithMany()
            .HasForeignKey(a => a.FollowingId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Direktemelding>()
            .HasOne(d => d.Avsender)
            .WithMany()
            .HasForeignKey(d => d.AvsenderId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Direktemelding>()
            .HasOne(d => d.Mottaker)
            .WithMany()
            .HasForeignKey(d => d.MottakerId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}