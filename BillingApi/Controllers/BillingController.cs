using BillingApi.Factories;
using BillingApi.Model;
using BillingApi.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BillingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillingController : ControllerBase
    {
        private readonly IOrderValidator _orderValidator;
        private readonly IPaymentGatewayFactory _paymentGatewayFactory;
        private readonly IPaymentOrderFactory _paymentOrderFactory;

        public BillingController(IOrderValidator orderValidator, IPaymentGatewayFactory paymentGatewayFactory, IPaymentOrderFactory paymentOrderFactory)
        {
            _orderValidator = orderValidator;
            _paymentGatewayFactory = paymentGatewayFactory;
            _paymentOrderFactory = paymentOrderFactory;
        }

        // POST api/<BillingController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Order order)
        {
            try
            {
                _orderValidator.Validate(order);

                var paymentGateway = _paymentGatewayFactory.Build(order.PaymentGateway);
                var paymentOrder = _paymentOrderFactory.Build(order);

                var result = await paymentGateway.ProcessAsync(paymentOrder);

                if (result.IsSuccess)
                {
                    return Ok(new Receipt { Id = result.TransactionId });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, result.Message);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
