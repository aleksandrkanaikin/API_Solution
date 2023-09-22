using Entities.Models;

namespace Contracts
{
    public interface IDriverRepository
    {
        IEnumerable<Driver> GetAllDrivers(bool trackChanges);
        public Driver GetDriver(Guid id, bool trackChanges);
    }
}
