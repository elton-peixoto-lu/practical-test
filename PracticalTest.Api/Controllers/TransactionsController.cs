using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PracticalTest.Api.CQRS.Commands;
using PracticalTest.Api.CQRS.Queries;
using PracticalTest.Api.Models;
using MediatR;

namespace PracticalTest.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransactionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllTransactionsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("ID não pode ser nulo ou vazio");
            }

            var query = new GetTransactionQuery(id);
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!transaction.IsValid())
            {
                return BadRequest("Transação inválida");
            }

            var command = new CreateTransactionCommand(transaction);
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.TransactionID }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Transaction transaction)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("ID não pode ser nulo ou vazio");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!transaction.IsValid())
            {
                return BadRequest("Transação inválida");
            }

            if (id != transaction.TransactionID)
            {
                return BadRequest("ID da URL não corresponde ao ID da transação");
            }

            var command = new UpdateTransactionCommand(transaction);
            var result = await _mediator.Send(command);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("ID não pode ser nulo ou vazio");
            }

            var command = new DeleteTransactionCommand(id);
            var result = await _mediator.Send(command);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
