namespace PizzaShop.Models
{
    public class OrderPizza
    {
        public OrderPizza(int OrderPizzaId,int OrderId, int PizzaId, int Count)
        {
            this.OrderPizzaId = OrderPizzaId;
            this.OrderId = OrderId;
            this.PizzaId = PizzaId;
            this.Count = Count;
        }
        public int OrderPizzaId { get; set; }
        public int OrderId { get; set; }
        public int PizzaId { get; set; }
        public int Count { get; set; }
    }
}
