namespace Florist.Infrastructure.Cqrs.Types
{
    public interface ICommand
    {
    }

    public interface ICommand<TResult> : ICommand
    {
    }
}
