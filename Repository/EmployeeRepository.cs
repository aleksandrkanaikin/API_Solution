using Contracts;
using Entities.Models;
using Entities;

namespace Repository
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public void Create1(Employee employee) => Create(employee);
    }
}
