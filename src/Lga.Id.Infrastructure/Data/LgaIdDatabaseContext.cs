
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Lga.Id.Core.Entities;
using System.Reflection;
using Lga.Id.Core.Entities.ScoreAggregate;
using Lga.Id.Infrastructure.Data.EntityConfigurations;

namespace Lga.Id.Infrastructure.Data
{
    public class LgaIdDatabaseContext : DbContext
    {
        public LgaIdDatabaseContext(DbContextOptions<LgaIdDatabaseContext> options) : base(options)
        {
        }

        public DbSet<State> States { get; set; }
        public DbSet<Location> Locations { get; set; }

        public DbSet<Score> Products { get; set; }
        public DbSet<ScoreDetail> ProductOptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ScoreConfiguration());
            modelBuilder.ApplyConfiguration(new ScoreConfiguration());
            modelBuilder.ApplyConfiguration(new ScoreConfiguration());
            modelBuilder.ApplyConfiguration(new ScoreDetailConfiguration());
        }
   
    }

   
}