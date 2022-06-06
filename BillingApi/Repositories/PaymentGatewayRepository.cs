using GatewayLibrary;
using PaymentGateways;
using System.Collections.Generic;

namespace BillingApi.Repositories
{
    public class PaymentGatewayRepository : IPaymentGatewayRepository
    {
        private readonly Dictionary<string, IPaymentGateway> data = new Dictionary<string, IPaymentGateway>();

        public PaymentGatewayRepository()
        {
            data.Add("FastCash", new FastCashPaymentGateway());
            data.Add("PayFast", new PayFastPaymentGateway());
        }

        public IPaymentGateway GetByName(string name)
        {
            return data.ContainsKey(name) ? data[name] : null;
        }

        public void Add(string name, IPaymentGateway paymentGateway)
        {
            data.Add(name, paymentGateway);
        }
    }
}
