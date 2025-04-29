namespace WebApplication1.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
