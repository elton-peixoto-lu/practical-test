using System.Collections.Generic;
using System.Threading.Tasks;
using PracticalTest.Api.Models;

namespace PracticalTest.Api.Data
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetAllAsync();
        Task<Transaction> GetByIdAsync(string id);
        Task<Transaction> CreateAsync(Transaction transaction);
        Task<Transaction> UpdateAsync(Transaction transaction);
        Task<bool> DeleteAsync(string id);
    }
} 
