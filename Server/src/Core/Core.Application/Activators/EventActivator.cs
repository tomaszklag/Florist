using Core.Application.Command;
using Core.Domain.Events;
using System;

namespace Core.Application.Activators
{
    public class EventActivator : IEventActivator
    {
        public CommandEvent<IEvent> CreateCommandEvent(IEvent @event)
        {
            var generic = typeof(CommandEvent<>);
            //return CreateEvent(generic, @event);
            return null;
        }

        private dynamic CreateEvent(Type generic, IEvent @event)
        {
            var type = @event.GetType();
            var genericType = generic.MakeGenericType(type);
            return Activator.CreateInstance(genericType, @event);
        }
    }
}
