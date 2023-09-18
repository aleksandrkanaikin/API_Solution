using Entities.Models;

namespace Contracts
{
    public interface ICompanyRepository
    {
        public void Delete(Company company);
        IEnumerable<Company> GetAllCompanies(bool trackChanges);
        Company GetCompany(Guid companyId, bool trackChanges);
        void CreateCompany(Company company);
        IEnumerable<Company> GetByIds(IEnumerable<Guid> ids, bool trackChanges);

    }
}
