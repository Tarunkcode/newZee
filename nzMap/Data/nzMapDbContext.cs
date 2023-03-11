using Microsoft.EntityFrameworkCore;
using nzMap.Model.Domain;

namespace nzMap.Data
{
    public class nzMapDbContext : DbContext
    {
        public nzMapDbContext(DbContextOptions<nzMapDbContext> options) : base(options)
        {

        }

        public DbSet<Region> Regions { get; set; }
        
        public DbSet<Walk> Walks { get; set; }
        
        public DbSet<WalkDifficulty> WalksDifficulty { get; set; }


    }
}
