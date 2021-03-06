using System;

namespace Domain
{
    public class CustomerCartDomain
    {
        public CustomerCartDomain()
        {
            cartattribute = new Cartattribute();
        }
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public string ProductId { get; set; }
        public string ITEM_DESC { get; set; }
        public DateTime CreatedDatetime { get; set; }
        public string IsDeleted { get; set; }
        public string AttributeValue { get; set; }
        public string AttributeType { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public bool IsPlace { get; set; }

        public Cartattribute cartattribute { get; set; }

    }
    public class Cartattribute
    {
        public string AttributeValue { get; set; }
        public string AttributeType { get; set; }

    }
}