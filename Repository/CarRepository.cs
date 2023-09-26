using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class CarRepository: RepositoryBase<Car>, ICarRepository
    {
        public CarRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Car>> GetCarsAsync(Guid driverId, bool trackChanges) => 
            await FindByCondition(c => c.DriverId.Equals(driverId), trackChanges).OrderBy(e => e.Brend).ToListAsync();
        public async Task<Car> GetCarByIdAsync(Guid driverId, Guid id, bool trackChanges) => await FindByCondition(c => c.DriverId.Equals(driverId) &&
            c.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
        public void CreateCarForDriver(Guid driverId, Car car)
        {
            car.DriverId = driverId;
            Create(car);
        }
        public void DeleteCar(Car car) => Delete(car);
    }
}
