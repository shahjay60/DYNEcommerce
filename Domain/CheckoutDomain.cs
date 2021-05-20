using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class CheckoutDomain
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Pincode { get; set; }
        public string ShippingAddress { get; set; }
        public string BillingAddress { get; set; }
        public Nullable<System.DateTime> RegistrationDatetime { get; set; }
        public string Password { get; set; }

        public int cartId { get; set; }
        public string CustomerId { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }

        public bool IsPlaced { get; set; }
        public List<CustomerCartDomain> customercart { get; set; }
    }
}
