using BillingApi.Repositories;
using GatewayLibrary;
using System;

namespace BillingApi.Factories
{
    /// <summary>
    /// Standard <seealso cref="IPaymentGateway"/> factory
    /// </summary>
    public class StandardPaymentGatewayFactory : IPaymentGatewayFactory
    {
        private readonly IPaymentGatewayRepository _gatewayRepository;

        public StandardPaymentGatewayFactory(IPaymentGatewayRepository gatewayRepository)
        {
            this._gatewayRepository = gatewayRepository;
        }

        public IPaymentGateway Build(string name)
        {
            var paymentGateway = _gatewayRepository.GetByName(name);
            if (paymentGateway == null)
            {
                throw new ArgumentException($"Gateway '{name ?? "<null>"}' is unknown");
            }
            return paymentGateway;
        }
    }
}
