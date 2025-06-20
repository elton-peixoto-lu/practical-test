using PracticalTest.Api.Models;
using Xunit;

namespace PracticalTest.Tests
{
    public class TransactionTests
    {
        [Fact]
        public void CanCreateTransaction()
        {
            var transaction = new Transaction { AccountID = "A1", TransactionID = "T1" };
            Assert.Equal("A1", transaction.AccountID);
        }
    }
}
