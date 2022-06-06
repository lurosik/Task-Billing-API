namespace BillingApi.Model
{
    /// <summary>
    /// Receipt (the result of <seealso cref="Order"/> processing)
    /// </summary>
    public class Receipt
    {
        /// <summary>
        /// Identifier (i.e. Transaction id)
        /// </summary>
        public string Id { get; set; }
    }
}
