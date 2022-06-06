using System.ComponentModel.DataAnnotations;

namespace BillingApi.Model
{
    /// <summary>
    /// An order
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Order number
        /// </summary>
        [Required]
        public string Number { get; set; }

        /// <summary>
        /// User id
        /// </summary>
        [Required]
        public string UserId { get; set; }

        /// <summary>
        /// Payable amount
        /// </summary>
        [Required]
        public decimal PayableAmount { get; set; }

        /// <summary>
        /// Payment currency
        /// </summary>
        [Required]
        public string Currency { get; set; }

        /// <summary>
        /// Payment gateway (identifier to map appropriate payment gateway)
        /// </summary>
        [Required]
        public string PaymentGateway { get; set; }

        /// <summary>
        /// Optional description
        /// </summary>
        public string Description { get; set; }
    }
}
