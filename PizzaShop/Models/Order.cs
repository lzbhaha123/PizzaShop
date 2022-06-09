namespace PizzaShop.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string? ClientName { get; set; }
        public string? ClientPhone { get; set; }
        public string? Address { get; set; }
        public int Status { get; set; }

        
    }
}
