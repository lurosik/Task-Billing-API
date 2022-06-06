using System;
using System.Collections.Generic;
using System.Text;

namespace GatewayLibrary.Model
{
    public class PaymentResult
    {
        public string TransactionId { get; set; }

        public bool IsSuccess { get; set; }

        public string Message { get; set; }
    }
}
