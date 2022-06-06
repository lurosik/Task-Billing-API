using GatewayLibrary;
using GatewayLibrary.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateways
{
    /// <summary>
    /// Dummy implementation of artificial "Pay Fast Ltd." company payment gateway
    /// </summary>
    public class PayFastPaymentGateway : IPaymentGateway
    {
        public PaymentResult Process(PaymentOrder order)
        {
            return new PaymentResult()
            {
                IsSuccess = true,
                Message = "Transaction finished with success",
                TransactionId = $"PayFast-{DateTime.UtcNow.Ticks}",
            };
        }

        public Task<PaymentResult> ProcessAsync(PaymentOrder order)
        {
            return Task.FromResult(new PaymentResult()
            {
                IsSuccess = true,
                Message = "Transaction finished with success",
                TransactionId = $"PayFast-{DateTime.UtcNow.Ticks}",
            });
        }
    }
}
