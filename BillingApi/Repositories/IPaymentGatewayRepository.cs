using GatewayLibrary;

namespace BillingApi.Repositories
{
    /// <summary>
    /// Repository of <seealso cref="IPaymentGateway"/>
    /// </summary>
    public interface IPaymentGatewayRepository
    {
        /// <summary>
        /// Gets PaymentGateway from repository by Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IPaymentGateway GetByName(string name);

        /// <summary>
        /// Adds mapping between a gateway name and <seealso cref="IPaymentGateway"/> (implementation)
        /// </summary>
        /// <param name="name">gateway name</param>
        /// <param name="paymentGateway">implementation</param>
        void Add(string name, IPaymentGateway paymentGateway);
    }
}