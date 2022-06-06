using BillingApi.Model;
using BillingApi.Validators;
using NUnit.Framework;
using FluentAssertions;
using System;

namespace BillingApiTests.Validators
{
    public class OrderValidatorTest
    {
        private OrderValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new OrderValidator();
        }

        [TestCase(-12.34)]
        [TestCase(0.0)]
        public void Should_throw_an_exception_when_order_payable_amount_value_is_incorrect(decimal payableAmount)
        {
            // arrange
            var order = new Order()
            {
                PayableAmount = payableAmount
            };

            // act
            var ex = Assert.Throws<ArgumentException>(() => _validator.Validate(order));

            // assert
            ex.Message.Should().Be("PayableAmount is not a positive value");
        }

        [Test]
        public void Should_not_throw_an_exception_when_order_is_correct()
        {
            // arrange
            var validator = new OrderValidator();
            var order = new Order()
            {
                Number = "ABC/123",
                Currency = "EUR",
                Description = null,
                PayableAmount = 12.34m,
                PaymentGateway = "X",
                UserId = "789-XYZ"
            };

            // act / assert
            Assert.DoesNotThrow(() => validator.Validate(order));
        }

    }
}
