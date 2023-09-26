﻿using Entities.Models;

namespace Contracts
{
    public interface ICompanyRepository
    {
        public void Delete(Company company);
        Task<IEnumerable<Company>> GetAllCompaniesAsync(bool trackChanges);
        Task<Company> GetCompanyAsync(Guid companyId, bool trackChanges);
        void CreateCompany(Company company);
        Task<IEnumerable<Company>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);

    }
}
