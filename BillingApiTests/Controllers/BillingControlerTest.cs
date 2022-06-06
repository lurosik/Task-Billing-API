using BillingApi.Controllers;
using BillingApi.Factories;
using BillingApi.Model;
using BillingApi.Validators;
using FluentAssertions;
using GatewayLibrary;
using GatewayLibrary.Model;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BillingApiTests.Controllers
{
    public class BillingControlerTest
    {
        private IOrderValidator _successfulOrderValidator;
        private IOrderValidator _notSuccessfulOrderValidator;

        [SetUp]
        public void SetUp()
        {
            _successfulOrderValidator = Substitute.For<IOrderValidator>();
            _successfulOrderValidator.Validate(Arg.Any<Order>());

            _notSuccessfulOrderValidator = Substitute.For<IOrderValidator>();
            _notSuccessfulOrderValidator.When(x => x.Validate(Arg.Any<Order>())).Throw(new ArgumentException("Invalid order"));
        }

        [Test]
        public async Task Successful_billing_order_processing_should_return_ok_status_and_transaction_id()
        {
            // arrange
            var paymentGateway = Substitute.For<IPaymentGateway>();
            paymentGateway.ProcessAsync(Arg.Any<PaymentOrder>()).Returns(Task.FromResult(new PaymentResult()
            {
                IsSuccess = true,
                Message = "Transaction finished with success",
                TransactionId = "Test",
            }));

            var paymentGatewayFactory = Substitute.For<IPaymentGatewayFactory>();
            paymentGatewayFactory.Build(Arg.Any<string>()).Returns(paymentGateway);

            var paymentOrderFactory = Substitute.For<IPaymentOrderFactory>();
            paymentOrderFactory.Build(Arg.Any<Order>()).Returns(new PaymentOrder()
            {
                Currency = "X",
                PayableAmount = 1.23m
            });

            var order = new Order()
            {
                Currency = "X",
                Number = "A/1B",
                PayableAmount = 1.23m,
                PaymentGateway = "test",
                UserId = "User123"
            };

            var controller = new BillingController(_successfulOrderValidator, paymentGatewayFactory, paymentOrderFactory);

            // act
            var result = await controller.Post(order);

            // assert
            result.Should().BeOfType<OkObjectResult>();
            var objResult = result as ObjectResult;
            objResult.StatusCode.Should().Be(200);
            objResult.Value.Should().BeOfType<Receipt>();
            var receipt = objResult.Value as Receipt;
            receipt.Id.Should().Be("Test");
        }

        [Test]
        public async Task Not_successful_billing_order_processing_should_return_error_status()
        {
            // arrange
            var paymentGateway = Substitute.For<IPaymentGateway>();
            paymentGateway.ProcessAsync(Arg.Any<PaymentOrder>()).Returns(Task.FromResult(new PaymentResult()
            {
                IsSuccess = false,
                Message = "There was an error",
                TransactionId = null,
            }));

            var paymentGatewayFactory = Substitute.For<IPaymentGatewayFactory>();
            paymentGatewayFactory.Build(Arg.Any<string>()).Returns(paymentGateway);

            var paymentOrderFactory = Substitute.For<IPaymentOrderFactory>();
            paymentOrderFactory.Build(Arg.Any<Order>()).Returns(new PaymentOrder()
            {
                Currency = "X",
                PayableAmount = 1.23m
            });

            var order = new Order()
            {
                Currency = "X",
                Number = "A/1B",
                PayableAmount = 1.23m,
                PaymentGateway = "test",
                UserId = "User123"
            };

            var controller = new BillingController(_successfulOrderValidator, paymentGatewayFactory, paymentOrderFactory);

            // act
            var result = await controller.Post(order);

            // assert
            result.Should().BeOfType<ObjectResult>();
            var objResult = result as ObjectResult;
            objResult.StatusCode.Should().Be(500);
            objResult.Value.Should().Be("There was an error");
        }


        [Test]
        public async Task Invalid_billing_order_should_return_error_status()
        {
            // arrange
            var paymentGateway = Substitute.For<IPaymentGateway>();
            paymentGateway.ProcessAsync(Arg.Any<PaymentOrder>()).Returns(Task.FromResult(new PaymentResult()
            {
                IsSuccess = false,
                Message = "There was an error",
                TransactionId = null,
            }));

            var paymentGatewayFactory = Substitute.For<IPaymentGatewayFactory>();
            paymentGatewayFactory.Build(Arg.Any<string>()).Returns(paymentGateway);

            var paymentOrderFactory = Substitute.For<IPaymentOrderFactory>();
            paymentOrderFactory.Build(Arg.Any<Order>()).Returns(new PaymentOrder()
            {
                Currency = "X",
                PayableAmount = 0.0m
            });

            var order = new Order()
            {
                Currency = "X",
                Number = "A/1B",
                PayableAmount = 0.0m,
                PaymentGateway = "test",
                UserId = "User123"
            };

            var controller = new BillingController(_notSuccessfulOrderValidator, paymentGatewayFactory, paymentOrderFactory);

            // act
            var result = await controller.Post(order);

            // assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var objResult = result as ObjectResult;
            objResult.StatusCode.Should().Be(400);
            objResult.Value.Should().Be("Invalid order");
        }

    }
}
