using Core.Application.Command;
using Core.Application.Event;
using Core.Domain.Events;
using System.Collections.Generic;

namespace Core.Application.Activators
{
    public interface IHandlerActivator
    {
        dynamic ResolveCommandHandler<T>(T command) where T : ICommand;

        IEnumerable<IEventHandler<T>> ResolveEventHandlers<T>(T @event) where T : IEvent;
    }
}
