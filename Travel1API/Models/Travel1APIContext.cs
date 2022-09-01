#nullable disable

using Microsoft.EntityFrameworkCore;
#nullable disable

namespace Travel1API.Models
{
  public class Travel1APIContext : DbContext
  {
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Destination> Destinations { get; set; }

    public Travel1APIContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<Review>()
          .HasData(
              new Review { ReviewId = 1, Description = "Bad roads", Rating = 2, DestinationId = 2 },
              new Review { ReviewId = 2, Description = "Tasty food", Rating = 3, DestinationId = 1 },
              new Review { ReviewId = 3, Description = "Fast policia", Rating = 4, DestinationId = 3 },
              new Review { ReviewId = 4, Description = "Expensive taxi", Rating = 5, DestinationId = 4 },
              new Review { ReviewId = 5, Description = "Danger neighborhood", Rating = 4, DestinationId = 5 }
          );

      builder.Entity<Destination>()
          .HasData(
              new Destination { DestinationId = 1, DestinationName = "London" },
              new Destination { DestinationId = 2, DestinationName = "Algeria" },
              new Destination { DestinationId = 3, DestinationName = "Australia" },
              new Destination { DestinationId = 4, DestinationName = "Chicago" },
              new Destination { DestinationId = 5, DestinationName = "Los Angeles" }
          );

   
    }
  }
}      