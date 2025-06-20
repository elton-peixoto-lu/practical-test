using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PracticalTest.Api.Data;
using PracticalTest.Api.Models;

namespace PracticalTest.Api.CQRS.Queries
{
    public class GetTransactionQuery : IRequest<Transaction>
    {
        public string Id { get; }

        public GetTransactionQuery(string id)
        {
            Id = id;
        }
    }

    public class GetTransactionQueryHandler : IRequestHandler<GetTransactionQuery, Transaction>
    {
        private readonly ITransactionRepository _repository;

        public GetTransactionQueryHandler(ITransactionRepository repository)
        {
            _repository = repository;
        }

        public async Task<Transaction> Handle(GetTransactionQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(request.Id);
        }
    }
} 
