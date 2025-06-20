using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PracticalTest.Api.Data;
using PracticalTest.Api.Models;

namespace PracticalTest.Api.CQRS.Commands
{
    public class CreateTransactionCommand : IRequest<Transaction>
    {
        public Transaction Transaction { get; }

        public CreateTransactionCommand(Transaction transaction)
        {
            Transaction = transaction;
        }
    }

    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, Transaction>
    {
        private readonly ITransactionRepository _repository;

        public CreateTransactionCommandHandler(ITransactionRepository repository)
        {
            _repository = repository;
        }

        public async Task<Transaction> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            return await _repository.CreateAsync(request.Transaction);
        }
    }
}
