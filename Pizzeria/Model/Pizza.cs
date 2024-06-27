namespace Pizzeria.Model
{
    public class Pizza
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool CreatedByUser { get; set; }
        public List<Ingredient> Ingredients { get; set; }

    }
}
