using Florist.Core.Types;
using Florist.Core.Types.Domain;
using System;
using System.Threading.Tasks;

namespace Florist.Services.Products
{
    public class TestCreateCommandHandler : ICommandHandler<TestCreateCommand>
    {
        private readonly IRepository _repository;

        public TestCreateCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public Task HandleAsync(TestCreateCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
