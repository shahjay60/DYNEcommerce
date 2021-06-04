using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class CartAttribute
    {
        public int Id { get; set; }
        public int cartId { get; set; }
        public string AttributeValueID { get; set; }
    }
}
