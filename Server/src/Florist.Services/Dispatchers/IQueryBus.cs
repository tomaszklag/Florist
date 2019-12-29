using Florist.Core.Types;
using System.Threading.Tasks;

namespace Florist.Services.Dispatchers
{
    public interface IQueryBus
    {
        Task<TResult> QueryAsync<TResult>(IQuery<TResult> query);
    }
}
