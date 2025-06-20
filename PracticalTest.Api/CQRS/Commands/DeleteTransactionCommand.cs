using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PracticalTest.Api.Data;

namespace PracticalTest.Api.CQRS.Commands
{
    public class DeleteTransactionCommand : IRequest<bool>
    {
        public string Id { get; }

        public DeleteTransactionCommand(string id)
        {
            Id = id;
        }
    }

    public class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionCommand, bool>
    {
        private readonly ITransactionRepository _repository;

        public DeleteTransactionCommandHandler(ITransactionRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
        {
            return await _repository.DeleteAsync(request.Id);
        }
    }
} 
