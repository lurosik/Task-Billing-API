using BillingApi.Model;
using BillingApi.Validators;
using NUnit.Framework;
using FluentAssertions;
using System.Linq;

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
        public void Should_return_valid_false_with_error_message_when_order_payable_amount_value_is_incorrect(decimal payableAmount)
        {
            // arrange
            var order = new Order()
            {
                PayableAmount = payableAmount
            };

            // act
            var result = _validator.Validate(order);

            // assert
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
            result.Errors.First().ErrorMessage.Should().Be("Payable Amount is not a positive value");
        }

        [Test]
        public void Should_return_valid_true_when_order_is_correct()
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

            // act
            var result = validator.Validate(order);

            // assert
            result.IsValid.Should().BeTrue();
        }

    }
}
