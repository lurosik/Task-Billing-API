using System;
using System.Collections.Generic;
using System.Text;

namespace GatewayLibrary.Model
{
    public class PaymentOrder
    {
        public decimal PayableAmount { get; set; }

        public string Currency { get; set; }
    }
}
