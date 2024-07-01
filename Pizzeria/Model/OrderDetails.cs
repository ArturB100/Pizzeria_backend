namespace Pizzeria.Model;

public class OrderDetails
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public Pizza Pizza { get; set; }
    public int Quantity { get; set; }
    public SizeEnum Size { get; set; }
}