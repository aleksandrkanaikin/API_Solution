using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class CarRepository: RepositoryBase<Car>, ICarRepository
    {
        public CarRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public async Task<PagedList<Car>> GetCarsAsync(Guid driverId, CarParameters carParameters, bool trackChanges)
        {
            var cars = await FindByCondition(c => c.DriverId.Equals(driverId) && (c.Brend[0] >= carParameters.FirstCarBrand[0] && c.Brend[0] <= carParameters.LastCarBrand[0]),
                trackChanges).OrderBy(e => e.Brend).ToListAsync();
            return PagedList<Car>.ToPagedList(cars, carParameters.PageNumber, carParameters.PageSize);
        }
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
