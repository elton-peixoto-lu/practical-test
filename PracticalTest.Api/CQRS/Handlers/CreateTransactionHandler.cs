using MediatR;
using PracticalTest.Api.CQRS.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace PracticalTest.Api.CQRS.Handlers
{
    public class CreateTransactionHandler : IRequestHandler<CreateTransactionCommand, int>
    {
        public Task<int> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            // Lógica para inserir no banco usando Dapper
            return Task.FromResult(1); // Retorne o ID criado
        }
    }
}
