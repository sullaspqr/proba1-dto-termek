namespace proba1.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }

        public ICollection<CartItem>? CartItems { get; set; }
    }
}
