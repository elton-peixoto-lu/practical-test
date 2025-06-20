using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PracticalTest.Api.Controllers;
using PracticalTest.Api.CQRS.Commands;
using PracticalTest.Api.CQRS.Queries;
using PracticalTest.Api.Models;
using Xunit;

namespace PracticalTest.Tests.Controllers
{
    public class TransactionsControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly TransactionsController _controller;

        public TransactionsControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new TransactionsController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResultWithTransactions()
        {
            // Arrange
            var transactions = new List<Transaction>
            {
                new Transaction { TransactionID = "1", AccountID = "A1" },
                new Transaction { TransactionID = "2", AccountID = "A2" }
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllTransactionsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(transactions);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<Transaction>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetById_ExistingTransaction_ReturnsOkResult()
        {
            // Arrange
            var transaction = new Transaction { TransactionID = "1", AccountID = "A1" };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetTransactionQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(transaction);

            // Act
            var result = await _controller.GetById("1");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<Transaction>(okResult.Value);
            Assert.Equal("1", returnValue.TransactionID);
        }

        [Fact]
        public async Task GetById_NonExistingTransaction_ReturnsNotFound()
        {
            // Arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetTransactionQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Transaction)null);

            // Act
            var result = await _controller.GetById("1");

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Create_ValidTransaction_ReturnsCreatedResult()
        {
            // Arrange
            var transaction = new Transaction { TransactionID = "1", AccountID = "A1" };

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateTransactionCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(transaction);

            // Act
            var result = await _controller.Create(transaction);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<Transaction>(createdResult.Value);
            Assert.Equal("1", returnValue.TransactionID);
        }

        [Fact]
        public async Task Update_ValidTransaction_ReturnsOkResult()
        {
            // Arrange
            var transaction = new Transaction { TransactionID = "1", AccountID = "A1" };

            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateTransactionCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(transaction);

            // Act
            var result = await _controller.Update("1", transaction);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<Transaction>(okResult.Value);
            Assert.Equal("1", returnValue.TransactionID);
        }

        [Fact]
        public async Task Update_NonExistingTransaction_ReturnsNotFound()
        {
            // Arrange
            var transaction = new Transaction { TransactionID = "1", AccountID = "A1" };

            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateTransactionCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Transaction)null);

            // Act
            var result = await _controller.Update("1", transaction);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Delete_ExistingTransaction_ReturnsNoContent()
        {
            // Arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteTransactionCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.Delete("1");

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_NonExistingTransaction_ReturnsNotFound()
        {
            // Arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteTransactionCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.Delete("1");

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
} 
