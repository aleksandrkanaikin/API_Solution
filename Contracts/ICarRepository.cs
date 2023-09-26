using Entities.Models;

namespace Contracts
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> GetCarsAsync(Guid driverId, bool trackChanges);
        Task<Car> GetCarByIdAsync(Guid driverId, Guid carId, bool trackChanges);
        void CreateCarForDriver(Guid driverId, Car car);
        void DeleteCar(Car car);
    }
}
