using System.Threading.Tasks;

namespace Florist.Infrastructure.Cqrs.Types
{
    public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        Task<TResult> HandleAsync(TQuery query);
    }
}
