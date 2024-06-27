namespace Pizzeria.Model
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal PriceForSmall { get; set; }
        public decimal PriceForMedium { get; set; }
        public decimal PriceForBig { get; set; }
    }
}
