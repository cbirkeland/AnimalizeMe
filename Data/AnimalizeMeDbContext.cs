using Microsoft.EntityFrameworkCore;
using AnimalizeMe.Models;

namespace AnimalizeMe.Data
{
    public class AnimalizeMeDbContext : DbContext
    {
        public AnimalizeMeDbContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Restaurant> Restaurants { get; set; }
    }
}
