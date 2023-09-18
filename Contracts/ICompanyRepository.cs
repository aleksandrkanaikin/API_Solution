using Entities.Models;

namespace Contracts
{
    public interface ICompanyRepository
    {
        public void Delete(Company company);
        IEnumerable<Company> GetAllCompanies(bool trackChanges);
        Company GetCompany(Guid companyId, bool trackChanges);
    }
}
