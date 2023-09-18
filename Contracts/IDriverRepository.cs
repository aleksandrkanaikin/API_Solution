using Entities.Models;

namespace Contracts
{
    public interface IDriverRepository
    {
        IEnumerable<Driver> GetAllDrivers(bool trackChanges);
    }
}
