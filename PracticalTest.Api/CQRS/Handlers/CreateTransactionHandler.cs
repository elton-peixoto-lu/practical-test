using MediatR;
using PracticalTest.Api.CQRS.Commands;
using PracticalTest.Api.Models;
using PracticalTest.Api.Data;
using System.Threading;
using System.Threading.Tasks;

namespace PracticalTest.Api.CQRS.Handlers
{
    public class CreateTransactionHandler : IRequestHandler<CreateTransactionCommand, Transaction>
    {
        private readonly ITransactionRepository _repository;

        public CreateTransactionHandler(ITransactionRepository repository)
        {
            _repository = repository;
        }

        public async Task<Transaction> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            return await _repository.CreateAsync(request.Transaction);
        }
    }
}
