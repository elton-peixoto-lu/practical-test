using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PracticalTest.Api.Models;
using System.IO;

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

        public static void ImportFromSalesTxt(string filePath)
        {
            if (!File.Exists(filePath)) return;

            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                // Supondo que o arquivo seja CSV com os campos na ordem correta
                var parts = line.Split(',');
                if (parts.Length < 25) continue; // Ajuste conforme o número de campos

                var transaction = new Transaction
                {
                    AccountID = parts[0],
                    TransactionID = parts[1],
                    TransactionAmount = decimal.TryParse(parts[2], out var amt) ? amt : 0,
                    TransactionCurrencyCode = parts[3],
                    LocalHour = int.TryParse(parts[4], out var lh) ? lh : 0,
                    TransactionScenario = parts[5],
                    TransactionType = parts[6],
                    TransactionIPaddress = parts[7],
                    IpState = parts[8],
                    IpPostalCode = parts[9],
                    IpCountry = parts[10],
                    IsProxyIP = parts[11] == "1" || parts[11].ToLower() == "true",
                    BrowserLanguage = parts[12],
                    PaymentInstrumentType = parts[13],
                    CardType = parts[14],
                    PaymentBillingPostalCode = parts[15],
                    PaymentBillingState = parts[16],
                    PaymentBillingCountryCode = parts[17],
                    ShippingPostalCode = parts[18],
                    ShippingState = parts[19],
                    ShippingCountry = parts[20],
                    CvvVerifyResult = parts[21],
                    DigitalItemCount = int.TryParse(parts[22], out var dcnt) ? dcnt : 0,
                    PhysicalItemCount = int.TryParse(parts[23], out var pcnt) ? pcnt : 0,
                    TransactionDateTime = DateTime.TryParse(parts[24], out var dt) ? dt : DateTime.UtcNow
                };

                _transactions.Add(transaction);
            }
        }
    }
} 
