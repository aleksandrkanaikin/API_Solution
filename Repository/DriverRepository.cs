using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class DriverRepository: RepositoryBase<Driver>, IDriverRepository
    {
        public DriverRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)        
        {
        }

        public async Task<IEnumerable<Driver>> GetAllDriversAsync(bool trackChanges) => await FindAll(trackChanges).OrderBy(c => c.Name).ToListAsync();
        public async Task<Driver> GetDriverAsync(Guid id, bool trackChanges) => await FindByCondition(c => c.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
        public void CreateDriver(Driver driver) => Create(driver);
        public async Task<IEnumerable<Driver>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) => await FindByCondition(x => ids.Contains(x.Id), trackChanges).ToListAsync();
        public void DeleteDriver(Driver driver) => Delete(driver);
    }
}
