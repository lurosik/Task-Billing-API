using BillingApi.Factories;
using BillingApi.Model;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillingApiTests.Factories
{
    public class PaymentOrderFactoryTest
    {
        [TestCase("", 123.45)]
        [TestCase(null, 123.45)]
        [TestCase("USD", 123.45)]
        public void Should_return_paymentOrder_from_order(string currency, decimal payableAmount)
        {
            // arrange
            var factory = new StandardPaymentOrderFactory();
            var order = new Order()
            {
                PayableAmount = payableAmount,
                Currency = currency,
                Description = "free text",
                Number = "123/ABC",
                PaymentGateway = "XYZ",
                UserId = "User123"
            };

            // act
            var result = factory.Build(order);

            // assert
            result.Should().NotBeNull();
            result.Currency.Should().Be(currency);
            result.PayableAmount.Should().Be(payableAmount);
        }

        [Test]
        public void Should_throw_an_exception_when_null_order_is_given()
        {
            // arrange
            var factory = new StandardPaymentOrderFactory();

            // act
            Action act = () => factory.Build(null);

            // assert
            act.Should().Throw<ArgumentNullException>();
        }

    }
}
