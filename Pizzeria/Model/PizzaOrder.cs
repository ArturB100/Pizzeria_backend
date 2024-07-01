namespace Pizzeria.Model;

public class PizzaOrder
{
    public int Id { get; set; }
    public User User { get; set; }
    public Address Address { get; set; }
    public DateTime CreatedAt { get; set; }
    public double TotalPrice { get; set; }
    public OrderStatusEnum Status { get; set; }
}