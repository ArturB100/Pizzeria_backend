using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Model;

public class PizzaOrder
{
    [Required(ErrorMessage = "Id is required")]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "User is required")]
    public User User { get; set; }
    
    [Required(ErrorMessage = "Address is required")]
    public Address Address { get; set; }
    
    // [Required(ErrorMessage = "Total price is required")]
    public double TotalPrice { get; set; }
    
    [Required(ErrorMessage = "Status is required")]
    public OrderStatusEnum Status { get; set; }
    
    [Required(ErrorMessage = "Order details are required")]
    public List<OrderDetails> OrderDetails { get; set; }
}