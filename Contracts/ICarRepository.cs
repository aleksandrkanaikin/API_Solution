using Entities.Models;
using Entities.RequestFeatures;

namespace Contracts
{
    public interface ICarRepository
    {
        Task<PagedList<Car>> GetCarsAsync(Guid driverId, CarParameters carParameters, bool trackChanges);
        Task<Car> GetCarByIdAsync(Guid driverId, Guid carId, bool trackChanges);
        void CreateCarForDriver(Guid driverId, Car car);
        void DeleteCar(Car car);
    }
}
