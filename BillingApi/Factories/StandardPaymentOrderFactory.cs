using BillingApi.Model;
using GatewayLibrary.Model;
using System;

namespace BillingApi.Factories
{
    /// <summary>
    /// Standard <seealso cref="PaymentOrder"/> factory
    /// </summary>
    public class StandardPaymentOrderFactory : IPaymentOrderFactory
    {
        public PaymentOrder Build(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            return new PaymentOrder()
            {
                Currency = order.Currency,
                PayableAmount = order.PayableAmount,
            };
        }
    }
}
