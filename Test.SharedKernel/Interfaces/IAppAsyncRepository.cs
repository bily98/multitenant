using Ardalis.Specification;

namespace Test.SharedKernel.Interfaces
{
    public interface IAppAsyncRepository<T> : IRepositoryBase<T> where T : class
    {
    }
}
