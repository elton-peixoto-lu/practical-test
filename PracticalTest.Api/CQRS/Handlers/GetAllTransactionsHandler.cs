using MediatR;
using PracticalTest.Api.CQRS.Queries;
using PracticalTest.Api.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PracticalTest.Api.CQRS.Handlers
{
    public class GetAllTransactionsHandler : IRequestHandler<GetAllTransactionsQuery, IEnumerable<Transaction>>
    {
        public Task<IEnumerable<Transaction>> Handle(GetAllTransactionsQuery request, CancellationToken cancellationToken)
        {
            // Lógica para buscar no banco usando Dapper
            return Task.FromResult<IEnumerable<Transaction>>(new List<Transaction>());
        }
    }
}
