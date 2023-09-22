using Entities.Models;

namespace Contracts
{
    public interface ICarRepository
    {
        IEnumerable<Car> GetCars(Guid driverId, bool trackChanges);
        Car GetCarById(Guid driverId, Guid carId, bool trackChanges);
    }
}
