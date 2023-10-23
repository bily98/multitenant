using Ardalis.Specification;

namespace Test.SharedKernel.Interfaces
{
    public interface ISecurityAsyncRepository<T> : IRepositoryBase<T> where T : class
    {
    }
}
