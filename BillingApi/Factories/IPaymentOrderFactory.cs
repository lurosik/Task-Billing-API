using BillingApi.Model;
using GatewayLibrary.Model;

namespace BillingApi.Factories
{
    /// <summary>
    /// Factory for creating <seealso cref="PaymentOrder"/> from and <seealso cref="Order"/>
    /// </summary>
    public interface IPaymentOrderFactory
    {
        /// <summary>
        /// Builds <seealso cref="PaymentOrder"/> from an <seealso cref="Order"/>
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        PaymentOrder Build(Order order);
    }
}