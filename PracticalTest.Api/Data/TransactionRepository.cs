using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Oracle.ManagedDataAccess.Client;
using PracticalTest.Api.Models;

namespace PracticalTest.Api.Data
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<TransactionRepository> _logger;
        private readonly IMemoryCache _cache;
        private const string ALL_TRANSACTIONS_CACHE_KEY = "AllTransactions";
        private static readonly TimeSpan CACHE_DURATION = TimeSpan.FromMinutes(5);

        public TransactionRepository(
            IConfiguration configuration, 
            ILogger<TransactionRepository> logger,
            IMemoryCache cache)
        {
            // Try to get connection string from environment variable first
            _connectionString = Environment.GetEnvironmentVariable("OracleConnection")
                ?? configuration.GetConnectionString("OracleConnection");
            _logger = logger;
            _cache = cache;
        }

        private IDbConnection CreateConnection()
        {
            return new OracleConnection(_connectionString);
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Buscando todas as transações");
                
                if (_cache.TryGetValue(ALL_TRANSACTIONS_CACHE_KEY, out IEnumerable<Transaction> cachedTransactions))
                {
                    _logger.LogInformation("Retornando transações do cache");
                    return cachedTransactions;
                }

                using var connection = CreateConnection();
                var result = await connection.QueryAsync<Transaction>("SELECT * FROM Transactions");
                
                _cache.Set(ALL_TRANSACTIONS_CACHE_KEY, result, CACHE_DURATION);
                _logger.LogInformation("Encontradas {Count} transações e armazenadas em cache", result.Count());
                
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar todas as transações");
                throw;
            }
        }

        public async Task<Transaction> GetByIdAsync(string id)
        {
            try
            {
                _logger.LogInformation("Buscando transação com ID: {Id}", id);
                
                string cacheKey = $"Transaction_{id}";
                if (_cache.TryGetValue(cacheKey, out Transaction cachedTransaction))
                {
                    _logger.LogInformation("Retornando transação do cache");
                    return cachedTransaction;
                }

                using var connection = CreateConnection();
                var result = await connection.QueryFirstOrDefaultAsync<Transaction>(
                    "SELECT * FROM Transactions WHERE TransactionID = :id",
                    new { id });

                if (result == null)
                {
                    _logger.LogWarning("Transação com ID {Id} não encontrada", id);
                    return null;
                }

                _cache.Set(cacheKey, result, CACHE_DURATION);
                _logger.LogInformation("Transação com ID {Id} encontrada e armazenada em cache", id);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar transação com ID: {Id}", id);
                throw;
            }
        }

        public async Task<Transaction> CreateAsync(Transaction transaction)
        {
            try
            {
                _logger.LogInformation("Criando nova transação com ID: {Id}", transaction.TransactionID);
                using var connection = CreateConnection();
                var sql = @"INSERT INTO Transactions (
                    TransactionID, AccountID, TransactionAmount, TransactionCurrencyCode,
                    LocalHour, TransactionScenario, TransactionType, TransactionIPaddress,
                    IpState, IpPostalCode, IpCountry, IsProxyIP, BrowserLanguage,
                    PaymentInstrumentType, CardType, PaymentBillingPostalCode,
                    PaymentBillingState, PaymentBillingCountryCode, ShippingPostalCode,
                    ShippingState, ShippingCountry, CvvVerifyResult, DigitalItemCount,
                    PhysicalItemCount, TransactionDateTime
                ) VALUES (
                    :TransactionID, :AccountID, :TransactionAmount, :TransactionCurrencyCode,
                    :LocalHour, :TransactionScenario, :TransactionType, :TransactionIPaddress,
                    :IpState, :IpPostalCode, :IpCountry, :IsProxyIP, :BrowserLanguage,
                    :PaymentInstrumentType, :CardType, :PaymentBillingPostalCode,
                    :PaymentBillingState, :PaymentBillingCountryCode, :ShippingPostalCode,
                    :ShippingState, :ShippingCountry, :CvvVerifyResult, :DigitalItemCount,
                    :PhysicalItemCount, :TransactionDateTime
                )";

                await connection.ExecuteAsync(sql, transaction);
                
                // Invalidate cache
                _cache.Remove(ALL_TRANSACTIONS_CACHE_KEY);
                _cache.Set($"Transaction_{transaction.TransactionID}", transaction, CACHE_DURATION);
                
                _logger.LogInformation("Transação com ID {Id} criada com sucesso", transaction.TransactionID);
                return transaction;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar transação com ID: {Id}", transaction.TransactionID);
                throw;
            }
        }

        public async Task<Transaction> UpdateAsync(Transaction transaction)
        {
            try
            {
                _logger.LogInformation("Atualizando transação com ID: {Id}", transaction.TransactionID);
                using var connection = CreateConnection();
                var sql = @"UPDATE Transactions SET
                    AccountID = :AccountID,
                    TransactionAmount = :TransactionAmount,
                    TransactionCurrencyCode = :TransactionCurrencyCode,
                    LocalHour = :LocalHour,
                    TransactionScenario = :TransactionScenario,
                    TransactionType = :TransactionType,
                    TransactionIPaddress = :TransactionIPaddress,
                    IpState = :IpState,
                    IpPostalCode = :IpPostalCode,
                    IpCountry = :IpCountry,
                    IsProxyIP = :IsProxyIP,
                    BrowserLanguage = :BrowserLanguage,
                    PaymentInstrumentType = :PaymentInstrumentType,
                    CardType = :CardType,
                    PaymentBillingPostalCode = :PaymentBillingPostalCode,
                    PaymentBillingState = :PaymentBillingState,
                    PaymentBillingCountryCode = :PaymentBillingCountryCode,
                    ShippingPostalCode = :ShippingPostalCode,
                    ShippingState = :ShippingState,
                    ShippingCountry = :ShippingCountry,
                    CvvVerifyResult = :CvvVerifyResult,
                    DigitalItemCount = :DigitalItemCount,
                    PhysicalItemCount = :PhysicalItemCount,
                    TransactionDateTime = :TransactionDateTime
                    WHERE TransactionID = :TransactionID";

                var rowsAffected = await connection.ExecuteAsync(sql, transaction);
                if (rowsAffected == 0)
                {
                    _logger.LogWarning("Transação com ID {Id} não encontrada para atualização", transaction.TransactionID);
                    return null;
                }

                // Invalidate cache
                _cache.Remove(ALL_TRANSACTIONS_CACHE_KEY);
                _cache.Set($"Transaction_{transaction.TransactionID}", transaction, CACHE_DURATION);

                _logger.LogInformation("Transação com ID {Id} atualizada com sucesso", transaction.TransactionID);
                return transaction;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar transação com ID: {Id}", transaction.TransactionID);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(string id)
        {
            try
            {
                _logger.LogInformation("Deletando transação com ID: {Id}", id);
                using var connection = CreateConnection();
                var rowsAffected = await connection.ExecuteAsync(
                    "DELETE FROM Transactions WHERE TransactionID = :id",
                    new { id });

                if (rowsAffected == 0)
                {
                    _logger.LogWarning("Transação com ID {Id} não encontrada para deleção", id);
                    return false;
                }

                // Invalidate cache
                _cache.Remove(ALL_TRANSACTIONS_CACHE_KEY);
                _cache.Remove($"Transaction_{id}");

                _logger.LogInformation("Transação com ID {Id} deletada com sucesso", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar transação com ID: {Id}", id);
                throw;
            }
        }
    }
} 
