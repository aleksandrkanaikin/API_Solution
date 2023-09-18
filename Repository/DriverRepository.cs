using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class DriverRepository: RepositoryBase<Driver>, IDriverRepository
    {
        public DriverRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)        
        {
        }

        public IEnumerable<Driver> GetAllDrivers(bool trackChanges) => FindAll(trackChanges).OrderBy(c => c.Name).ToList();
    }
}
