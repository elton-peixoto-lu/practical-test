using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PracticalTest.Api.Data;
using PracticalTest.Api.Models;

namespace PracticalTest.Api.CQRS.Queries
{
    public class GetAllTransactionsQuery : IRequest<IEnumerable<Transaction>>
    {
    }

    public class GetAllTransactionsQueryHandler : IRequestHandler<GetAllTransactionsQuery, IEnumerable<Transaction>>
    {
        private readonly ITransactionRepository _repository;

        public GetAllTransactionsQueryHandler(ITransactionRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Transaction>> Handle(GetAllTransactionsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}
