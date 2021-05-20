using System;

namespace Domain
{
    public class CustomerCartDomain
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public string ProductId { get; set; }
        public string ITEM_DESC { get; set; }

        public DateTime CreatedDatetime { get; set; }
        public string IsDeleted { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public bool IsPlace { get; set; }
    }
}