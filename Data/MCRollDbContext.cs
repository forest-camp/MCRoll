using MCRoll.Models;
using Microsoft.EntityFrameworkCore;

namespace MCRoll.Data
{
    public class MCRollDbContext : DbContext
    {
        public DbSet<Roll> Rolls { get; set; }

        public DbSet<Participant> Users { get; set; }

        public DbSet<Winner> Winners { get; set; }

        public MCRollDbContext(DbContextOptions<MCRollDbContext> options)
           : base(options) { }

    }
}
