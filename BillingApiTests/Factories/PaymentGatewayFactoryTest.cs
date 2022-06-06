using BillingApi.Factories;
using BillingApi.Repositories;
using FluentAssertions;
using GatewayLibrary;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillingApiTests.Factories
{
    public class PaymentGatewayFactoryTest
    {
        [Test]
        public void Should_return_PaymentGateway_if_there_is_a_matching_name()
        {
            // arrange
            var gateway = Substitute.For<IPaymentGateway>();
            var repository = Substitute.For<IPaymentGatewayRepository>();
            repository.GetByName("test").Returns(gateway);

            var factory = new StandardPaymentGatewayFactory(repository);

            // act
            var result = factory.Build("test");

            // assert
            result.Should().NotBeNull();
            result.Should().Be(gateway);
        }

        [Test]
        public void Should_return_null_PaymentGateway_if_there_is_no_matching_name()
        {
            // arrange
            var gateway = Substitute.For<IPaymentGateway>();
            var repository = Substitute.For<IPaymentGatewayRepository>();
            repository.GetByName(Arg.Any<string>()).Returns(null as IPaymentGateway);

            var factory = new StandardPaymentGatewayFactory(repository);

            // act
            Action act = () => factory.Build("x");

            // assert
            act.Should().Throw<ArgumentException>().WithMessage("Gateway 'x' is unknown");
        }
    }
}
