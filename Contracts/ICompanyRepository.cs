using Entities.Models;

namespace Contracts
{
    public interface ICompanyRepository
    {
        public void Delete(Company company);
        IEnumerable<Company> GetAllCompanies(bool trackChanges);
    }
}
