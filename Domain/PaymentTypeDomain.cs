using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class PaymentTypeDomain
    {
        public int Id { get; set; }
        public string PaymentType { get; set; }
        public bool IsActive { get; set; }
    }
}
