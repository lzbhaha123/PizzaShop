namespace PizzaShop.Models
{
    public class Order
    {
        public Order(int OrderId,string ClientName, string ClientPhone,string Address,int Status)
        {
            this.OrderId = OrderId;
            this.ClientName = ClientName;  
            this.ClientPhone = ClientPhone; 
            this.Address = Address;
            this.Status = Status;
        }
        public int OrderId { get; set; }
        public string? ClientName { get; set; }
        public string? ClientPhone { get; set; }
        public string? Address { get; set; }
        public int Status { get; set; }

        
    }
}
