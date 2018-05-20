using Footy.Rest.Api.Players;
using Microsoft.EntityFrameworkCore;

namespace Footy.Rest.Api.General
{
    public class FootyContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        
        public FootyContext(DbContextOptions<FootyContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Player>();
        }
    }
}