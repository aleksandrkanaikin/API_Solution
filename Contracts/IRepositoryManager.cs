namespace Contracts
{
    public interface IRepositoryManager
    {
        ICompanyRepository Company { get; }
        IEmployeeRepository Employee { get; }
        ICarRepository Car { get; }
        IDriverRepository Driver { get; }
        public Task SaveAsync();
    }
}
