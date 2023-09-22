using Entities.Models;

namespace Contracts
{
    public interface ICarRepository
    {
        IEnumerable<Car> GetAllCars(bool trackChanges);
        Car GetCarById(Guid driverId, Guid carId, bool trackChanges);
    }
}
