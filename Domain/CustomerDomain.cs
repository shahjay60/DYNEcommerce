using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class CustomerDomain
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "FirstName Required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName Required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email address Required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Required")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Country Required")]
        public string Country { get; set; }

        [Required(ErrorMessage = "State Required")]
        public string State { get; set; }

        [Required(ErrorMessage = "City Required")]
        public string City { get; set; }

        [Required(ErrorMessage = "Pincode Required")]
        [DataType(DataType.PostalCode)]
        public string Pincode { get; set; }

        [Required(ErrorMessage = "Shipping Address Required")]
        public string ShippingAddress { get; set; }

        [Required(ErrorMessage = "Billing Address Required")]
        public string BillingAddress { get; set; }

        public Nullable<System.DateTime> RegistrationDatetime { get; set; }

        [Required(ErrorMessage = "Password Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
