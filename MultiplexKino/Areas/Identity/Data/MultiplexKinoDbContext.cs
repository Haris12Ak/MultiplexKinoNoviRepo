using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MultiplexKino.Areas.Identity.Data;
using MultiplexKino.Areas.Identity.Data.EntityModels;
using MultiplexKino.Models.Projekcija;

namespace MultiplexKino.Areas.Identity.Data;

public class MultiplexKinoDbContext : IdentityDbContext<MultiplexKinoUser>
{
    public MultiplexKinoDbContext(DbContextOptions<MultiplexKinoDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
    }

    public DbSet<Film> Film { get; set; }
    public DbSet<Projekcija> Projekcija { get; set; }
    public DbSet<Sala> Sala { get; set; }
    public DbSet<Zanr> Zanr { get; set; }
    public DbSet<Informacije> Informacije { get; set; }
    public DbSet<CinemaHallShowing> Category { get; set; }
    public DbSet<CinemaHall> CinemaHall { get; set; }
    public DbSet<MoviesForShowing> MoviesForShowing { get; set; }
    public DbSet<SeatsForHall> SeatsForHall { get; set; }
    public DbSet<Seats> Seats { get; set; }
    public DbSet<Food> Food { get; set; }
    public DbSet<Sjedalo> Sjedalo { get; set; }
    public DbSet<Rezervacija> Rezervacija { get; set; }
}


public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<MultiplexKinoUser>
{
    public void Configure(EntityTypeBuilder<MultiplexKinoUser> builder)
    {
        builder.Property(x => x.FirstName).HasMaxLength(255);
        builder.Property(x => x.LastName).HasMaxLength(255);

    }
}
