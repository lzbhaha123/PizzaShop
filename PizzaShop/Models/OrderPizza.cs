namespace PizzaShop.Models
{
    public class OrderPizza
    {
        public int OrderPizzaId { get; set; }
        public Order? Order { get; set; }
        public Pizza? Pizza { get; set; }
        public int Count { get; set; }
    }
}
