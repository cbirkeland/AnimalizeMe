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

        public DbSet<Creature> Creatures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CreatureTags>().HasKey(x => new { x.TagId, x.CreatureId });
        }
    }
}
