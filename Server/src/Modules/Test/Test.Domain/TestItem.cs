using Core.Domain.Entities;
using System;
using Test.Domain.Events;

namespace Test.Domain
{
    public class TestItem : Aggregate
    {
        public string Name { get; private set; }

        public void Create(Guid id, string name)
        {
            if (Version > 0)
                throw new InvalidOperationException($"Aggreagte already exists");

            var @event = new TestItemCreated(id, name);
            Publish(@event);
        }

        private void When(TestItemCreated e)
        {
            Id = e.Id;
            Name = e.Name;
        }
    }
}
