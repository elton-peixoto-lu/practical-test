using PracticalTest.Api.Models;
using Xunit;

namespace PracticalTest.Tests
{
    public class TransactionTests
    {
        [Fact]
        public void CanCreateTransaction()
        {
            var transaction = new Transaction { AccountID = 1, TransactionID = 1 };
            Assert.Equal(1, transaction.AccountID);
        }
    }
}
