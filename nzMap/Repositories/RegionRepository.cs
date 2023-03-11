using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nzMap.Data;
using nzMap.Model.Domain;

namespace nzMap.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly nzMapDbContext dbContext;

        public RegionRepository(nzMapDbContext db)
        {
            this.dbContext = db;
        }

        public async Task<bool> AddRegion(Region region)
        {
            region.Id = Guid.NewGuid();

            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Region> AddWithGetAddedRegion(Region region)
        {
            region.Id = Guid.NewGuid();
            await dbContext.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<IEnumerable<Region>> GetAll()
        {
            return await dbContext.Regions.ToListAsync();
        }
     
        public async Task<Region> GetRegion(Guid id)
        {
           var region = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
           return region;
           
        }

        public async Task<Region> DeleteRegion(Guid id)
        {
            var region = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (region == null) return null;
            else
            {
               dbContext.Regions.Remove(region);
                await dbContext.SaveChangesAsync();
                return region;
            }
        }
    }
}
