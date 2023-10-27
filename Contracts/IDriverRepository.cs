using System;
using Entities.Models;

namespace Contracts
{
    public interface IDriverRepository
    {
        Task<IEnumerable<Driver>> GetAllDriversAsync(bool trackChanges);
        public Task<Driver> GetDriverAsync(Guid id, bool trackChanges);
        void CreateDriver(Driver driver);       
        Task<IEnumerable<Driver>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteDriver(Driver driver);
    }
}
