﻿using Contracts;
using Entities.Models;
using Entities;

namespace Repository
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public void Delete1(Company company) => Delete(company); 
      
    }
}
