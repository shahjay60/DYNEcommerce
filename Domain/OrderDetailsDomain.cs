using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class OrderDetailsDomain
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OrderAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderCounty { get; set; }
        public string OrderState { get; set; }
        public string OrderCity { get; set; }
        public string OrderPostCode { get; set; }
        public string OrderEmail { get; set; }
        public string OrderPhone { get; set; }
        public string OrderShippingAddress { get; set; }
        public string OrderBillingAddress { get; set; }
        public string PaymentType { get; set; }
        public string TransactionId { get; set; }

        public string PaymentTYpe { get; set; }
        public string CardHolderName { get; set; }
        public string ExpDate { get; set; }
        public string CVV { get; set; }
        public string CardNumber { get; set; }
        public DateTime TransactionDate { get; set; }
        public int OrderId { get; set; }



    }
}
