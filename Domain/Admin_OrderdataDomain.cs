using System;

namespace Domain
{
    public class Admin_OrderdataDomain
    {
        public string CustomerId { get; set; }
        public string ProductId { get; set; }

        public string OrderId { get; set; }

        public string Item_desc { get; set; }
        public string IsPlaced { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderAmount { get; set; }

        public string PaymentType { get; set; }
        public string TransactionId { get; set; }
    }
}
