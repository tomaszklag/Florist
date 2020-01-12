using Core.Domain.Events;
using System;

namespace Test.Domain.Events
{
    public class TestItemCreated : IEvent
    {
     
        public Guid Id { get; }
        public string Name { get; }

        public TestItemCreated(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
