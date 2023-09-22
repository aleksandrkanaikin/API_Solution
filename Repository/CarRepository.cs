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

        public IEnumerable<Car> GetAllCars(bool trackChanges) => FindAll(trackChanges).OrderBy(c=> c.Brend).ToList();
        public Car GetCarById(Guid id, bool trackChanges) => FindByCondition(c => c.Id.Equals(id), trackChanges).SingleOrDefault(); 
    }
}
