using BillingApi.Model;

namespace BillingApi.Validators
{
    /// <summary>
    /// Order validator
    /// Used to execute additional validation of the order (i.e. multiple fields or for specific needs)
    /// </summary>
    public interface IOrderValidator
    {
        /// <summary>
        /// Validation method (if validation is successful then nothing happens, otherwise exeception shall be thrown)
        /// </summary>
        /// <param name="order"></param>
        void Validate(Order order);
    }
}