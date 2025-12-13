namespace proba1.DTOs
{
    public class CartItemDto
    {
        public int CartItemId { get; set; }

        public int CustomerId { get; set; }      // Új
        public string? CustomerName { get; set; }

        public int ProductId { get; set; }       // Új
        public string? ProductName { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public decimal Total => Price * Quantity;
    }
}
