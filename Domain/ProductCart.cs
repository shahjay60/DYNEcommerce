namespace Domain
{
    public class ProductCart
    {
        public decimal Amount { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public int Quantity { get; set; }
        public int CartId { get; set; }
        public int WishListId { get; set; }

        public string ProductId { get; set; }
        public int TotalAmount { get; set; }
    }
}
