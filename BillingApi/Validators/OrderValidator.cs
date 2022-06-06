using BillingApi.Model;
using System;

namespace BillingApi.Validators
{
    public class OrderValidator : IOrderValidator
    {
        public void Validate(Order order)
        {
            if (order.PayableAmount <= 0) throw new ArgumentException($"{nameof(order.PayableAmount)} is not a positive value");
        }
    }
}
