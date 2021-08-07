using System;

namespace Domain
{
    public class CustomerWishlistDomain
    {
        public int Id { get; set; }
        public int qty { get; set; }

        public int CustomerId { get; set; }
        public string ProductId { get; set; }
        public string ITEM_DESC { get; set; }

        public string Image { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public decimal Amount { get; set; }
        public string AttributeValues { get; set; }


    }
}
