using System;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public interface IRepository
    {
        Task<T> GetByIdAsync<T>(Guid id) where T : IAggregate, new();
        Task SaveAsync<T>(T aggregate) where T : IAggregate;
    }
}
