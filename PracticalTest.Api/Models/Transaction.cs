using System;
using System.ComponentModel.DataAnnotations;

namespace PracticalTest.Api.Models
{
    public class Transaction
    {
        [Key]
        [Required(ErrorMessage = "TransactionID é obrigatório")]
        public string TransactionID { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "AccountID é obrigatório")]
        public string AccountID { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Valor da transação é obrigatório")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Valor deve ser maior que zero")]
        public decimal TransactionAmount { get; set; }
        
        [Required(ErrorMessage = "Código da moeda é obrigatório")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Código da moeda deve ter 3 caracteres")]
        public string TransactionCurrencyCode { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Hora local é obrigatória")]
        [Range(0, 23, ErrorMessage = "Hora local deve estar entre 0 e 23")]
        public int LocalHour { get; set; }
        
        [Required(ErrorMessage = "Cenário da transação é obrigatório")]
        public string TransactionScenario { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Tipo da transação é obrigatório")]
        public string TransactionType { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "IP da transação é obrigatório")]
        [RegularExpression(@"^(?:[0-9]{1,3}\.){3}[0-9]{1,3}$", ErrorMessage = "IP inválido")]
        public string TransactionIPaddress { get; set; } = string.Empty;
        
        public string IpState { get; set; } = string.Empty;
        
        public string IpPostalCode { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "País do IP é obrigatório")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "Código do país deve ter 2 caracteres")]
        public string IpCountry { get; set; } = string.Empty;
        
        public bool IsProxyIP { get; set; }
        
        [Required(ErrorMessage = "Idioma do navegador é obrigatório")]
        public string BrowserLanguage { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Tipo do instrumento de pagamento é obrigatório")]
        public string PaymentInstrumentType { get; set; } = string.Empty;
        
        public string CardType { get; set; } = string.Empty;
        
        public string PaymentBillingPostalCode { get; set; } = string.Empty;
        
        public string PaymentBillingState { get; set; } = string.Empty;
        
        [StringLength(2, MinimumLength = 2, ErrorMessage = "Código do país de cobrança deve ter 2 caracteres")]
        public string PaymentBillingCountryCode { get; set; } = string.Empty;
        
        public string ShippingPostalCode { get; set; } = string.Empty;
        
        public string ShippingState { get; set; } = string.Empty;
        
        [StringLength(2, MinimumLength = 2, ErrorMessage = "Código do país de entrega deve ter 2 caracteres")]
        public string ShippingCountry { get; set; } = string.Empty;
        
        public string CvvVerifyResult { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Quantidade de itens digitais é obrigatória")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantidade de itens digitais deve ser maior ou igual a zero")]
        public int DigitalItemCount { get; set; }
        
        [Required(ErrorMessage = "Quantidade de itens físicos é obrigatória")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantidade de itens físicos deve ser maior ou igual a zero")]
        public int PhysicalItemCount { get; set; }
        
        [Required(ErrorMessage = "Data e hora da transação é obrigatória")]
        public DateTime TransactionDateTime { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(TransactionID) &&
                   !string.IsNullOrEmpty(AccountID) &&
                   TransactionAmount > 0 &&
                   TransactionDateTime != DateTime.MinValue;
        }
    }
}
