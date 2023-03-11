using nzMap.Model.Domain;

namespace nzMap.Repositories
{
    public interface IRegionRepository
    {
       Task<IEnumerable<Region>> GetAll();

       Task<Region> GetRegion(Guid id);

       Task<bool> AddRegion(Region region);
       Task<Region> AddWithGetAddedRegion(Region region);
       Task<Region> DeleteRegion (Guid id);
    }
}
