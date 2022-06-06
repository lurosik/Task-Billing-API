using GatewayLibrary.Model;
using System.Threading.Tasks;

namespace GatewayLibrary
{
    /// <summary>
    /// Payment gateway
    /// </summary>
    public interface IPaymentGateway
    {
        /// <summary>
        /// Processes a <seealso cref="PaymentOrder"/> and as a results returns information about it (<seealso cref="PaymentResult"/>)
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        PaymentResult Process(PaymentOrder order);

        /// <summary>
        /// Processes a <seealso cref="PaymentOrder"/> and as a results returns information about it (<seealso cref="PaymentResult"/>) in an async way
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        Task<PaymentResult> ProcessAsync(PaymentOrder order);
    }
}
