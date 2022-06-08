using BillingApi.Factories;
using BillingApi.Model;
using BillingApi.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BillingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillingController : ControllerBase
    {
        private readonly IValidator<Order> _orderValidator;
        private readonly IPaymentGatewayFactory _paymentGatewayFactory;
        private readonly IPaymentOrderFactory _paymentOrderFactory;
        private readonly ILogger<BillingController> _logger;

        public BillingController(IValidator<Order> orderValidator, IPaymentGatewayFactory paymentGatewayFactory, IPaymentOrderFactory paymentOrderFactory, ILogger<BillingController> logger)
        {
            _orderValidator = orderValidator;
            _paymentGatewayFactory = paymentGatewayFactory;
            _paymentOrderFactory = paymentOrderFactory;
            _logger = logger;
        }

        // POST api/<BillingController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Order order)
        {
            try
            {
                var validationResult = _orderValidator.Validate(order);
                if (!validationResult.IsValid)
                {
                    return BadRequest(string.Join(", ", validationResult.Errors.Select(x => x.ErrorMessage)));
                }

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
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
