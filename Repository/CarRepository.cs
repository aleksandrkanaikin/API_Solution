using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class CarRepository: RepositoryBase<Car>, ICarRepository
    {
        public CarRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public IEnumerable<Car> GetCars(Guid driverId, bool trackChanges) => 
            FindByCondition(c => c.DriverId.Equals(driverId), trackChanges).OrderBy(e => e.Brend);
        public Car GetCarById(Guid driverId, Guid id, bool trackChanges) => FindByCondition(c => c.DriverId.Equals(driverId) &&
            c.Id.Equals(id), trackChanges).SingleOrDefault();
        public void CreateCarForDriver(Guid driverId, Car car)
        {
            car.DriverId = driverId;
            Create(car);
        }
        public void DeleteCar(Car car) => Delete(car);
    }
}
