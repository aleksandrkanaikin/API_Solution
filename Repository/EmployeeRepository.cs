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

        public IEnumerable<Employee> GetEmployees(Guid companyId, bool trackChanges) => FindByCondition(e => 
        e.CompanyId.Equals(companyId), trackChanges).OrderBy(e => e.Name);
        public Employee GetEmployee(Guid companyId, Guid id, bool trackChanges) => FindByCondition(e => e.CompanyId.Equals(companyId) && 
        e.Id.Equals(id), trackChanges).SingleOrDefault();
    }
}
