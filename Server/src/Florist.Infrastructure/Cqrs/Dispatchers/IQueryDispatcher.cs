using Florist.Core.Types;
using System.Threading.Tasks;

namespace Florist.Infrastructure.Cqrs.Dispatchers
{
    public interface IQueryDispatcher
    {
        Task<TResult> QueryAsync<TResult>(IQuery<TResult> query);
    }
}
