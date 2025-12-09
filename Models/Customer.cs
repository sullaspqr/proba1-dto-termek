namespace proba1.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public ICollection<CartItem>? CartItems { get; set; }
    }
}
