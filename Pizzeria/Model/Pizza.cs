using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Model
{
    public class Pizza
    {    
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public bool CreatedByUser { get; set; }
        
        [Required(ErrorMessage = "Ingredients are required")]
        public List<Ingredient> Ingredients { get; set; }

    }
}
