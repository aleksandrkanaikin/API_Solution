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
    }
}
