using System.Threading.Tasks;
using Core.Application.Command;
using Core.Domain.Entities;
using Test.Domain;

namespace Test.Application.Commands.CreateTest
{
    class CreateTestCommandHandler : ICommandHandler<CreateTestCommand>
    {
        private readonly IRepository _repository;

        public CreateTestCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task HandleAsync(CreateTestCommand command)
        {
            var test = await _repository.GetByIdAsync<TestItem>(command.Id);
            
            test.Create(command.Id, command.Name);

            await _repository.SaveAsync(test);
        }
    }
}
