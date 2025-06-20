using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PracticalTest.Api.Data;
using PracticalTest.Api.Models;

namespace PracticalTest.Api.CQRS.Commands
{
    public class UpdateTransactionCommand : IRequest<Transaction>
    {
        public Transaction Transaction { get; }

        public UpdateTransactionCommand(Transaction transaction)
        {
            Transaction = transaction;
        }
    }

    public class UpdateTransactionCommandHandler : IRequestHandler<UpdateTransactionCommand, Transaction>
    {
        private readonly ITransactionRepository _repository;

        public UpdateTransactionCommandHandler(ITransactionRepository repository)
        {
            _repository = repository;
        }

        public async Task<Transaction> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
        {
            return await _repository.UpdateAsync(request.Transaction);
        }
    }
} 
