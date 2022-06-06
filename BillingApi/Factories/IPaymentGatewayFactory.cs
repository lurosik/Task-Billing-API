using GatewayLibrary;

namespace BillingApi.Factories
{
    /// <summary>
    /// Factory for constructing <seealso cref="IPaymentGateway"/> based on some "rules"
    /// </summary>
    public interface IPaymentGatewayFactory
    {
        /// <summary>
        /// Builds a <seealso cref="IPaymentGateway"/> based on the given name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IPaymentGateway Build(string name);
    }
}