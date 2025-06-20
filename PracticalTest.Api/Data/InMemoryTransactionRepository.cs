using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PracticalTest.Api.Models;

namespace PracticalTest.Api.Data
{
    public class InMemoryTransactionRepository : ITransactionRepository
    {
        private static readonly List<Transaction> _transactions = new();

        public Task<IEnumerable<Transaction>> GetAllAsync()
            => Task.FromResult(_transactions.AsEnumerable());

        public Task<Transaction> GetByIdAsync(string id)
            => Task.FromResult(_transactions.FirstOrDefault(t => t.TransactionID == id));

        public Task<Transaction> CreateAsync(Transaction transaction)
        {
            _transactions.Add(transaction);
            return Task.FromResult(transaction);
        }

        public Task<Transaction> UpdateAsync(Transaction transaction)
        {
            var idx = _transactions.FindIndex(t => t.TransactionID == transaction.TransactionID);
            if (idx == -1) return Task.FromResult<Transaction>(null);
            _transactions[idx] = transaction;
            return Task.FromResult(transaction);
        }

        public Task<bool> DeleteAsync(string id)
        {
            var t = _transactions.FirstOrDefault(t => t.TransactionID == id);
            if (t == null) return Task.FromResult(false);
            _transactions.Remove(t);
            return Task.FromResult(true);
        }

        // Método para popular dados de exemplo
        public static void SeedExampleData()
        {
            if (_transactions.Count == 0)
            {
                _transactions.AddRange(new[]
                {
                    new Transaction {
                        TransactionID = "T1",
                        AccountID = "A1",
                        TransactionAmount = 100.50m,
                        TransactionCurrencyCode = "USD",
                        LocalHour = 14,
                        TransactionScenario = "Online",
                        TransactionType = "Purchase",
                        TransactionIPaddress = "192.168.1.1",
                        IpState = "CA",
                        IpPostalCode = "90001",
                        IpCountry = "US",
                        IsProxyIP = false,
                        BrowserLanguage = "en-US",
                        PaymentInstrumentType = "CreditCard",
                        CardType = "Visa",
                        PaymentBillingPostalCode = "90001",
                        PaymentBillingState = "CA",
                        PaymentBillingCountryCode = "US",
                        ShippingPostalCode = "90001",
                        ShippingState = "CA",
                        ShippingCountry = "US",
                        CvvVerifyResult = "M",
                        DigitalItemCount = 1,
                        PhysicalItemCount = 2,
                        TransactionDateTime = DateTime.UtcNow.AddDays(-1)
                    },
                    new Transaction {
                        TransactionID = "T2",
                        AccountID = "A2",
                        TransactionAmount = 250.00m,
                        TransactionCurrencyCode = "EUR",
                        LocalHour = 9,
                        TransactionScenario = "InStore",
                        TransactionType = "Refund",
                        TransactionIPaddress = "10.0.0.2",
                        IpState = "SP",
                        IpPostalCode = "01000-000",
                        IpCountry = "BR",
                        IsProxyIP = true,
                        BrowserLanguage = "pt-BR",
                        PaymentInstrumentType = "DebitCard",
                        CardType = "Mastercard",
                        PaymentBillingPostalCode = "01000-000",
                        PaymentBillingState = "SP",
                        PaymentBillingCountryCode = "BR",
                        ShippingPostalCode = "01000-000",
                        ShippingState = "SP",
                        ShippingCountry = "BR",
                        CvvVerifyResult = "N",
                        DigitalItemCount = 0,
                        PhysicalItemCount = 1,
                        TransactionDateTime = DateTime.UtcNow.AddDays(-2)
                    }
                });
            }
        }
    }
} 
