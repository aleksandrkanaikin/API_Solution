using Entities.Models;

namespace Contracts
{
    public interface ICarRepository
    {
        IEnumerable<Car> GetAllCars(bool trackChanges);
        Car GetCarById(Guid carId, bool trackChanges);
    }
}
