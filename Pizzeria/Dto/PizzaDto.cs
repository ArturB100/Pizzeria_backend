using Pizzeria.Model;

namespace Pizzeria.Dto
{
    public record PizzaDto (int Id, string Name, bool CreateByUser, List<Ingredient> Ingredients, decimal TotalPriceForSmall, decimal TotalPriceForMedium, decimal TotalPriceForBig);
}
