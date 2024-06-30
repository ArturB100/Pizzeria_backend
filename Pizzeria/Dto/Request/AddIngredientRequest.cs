namespace Pizzeria.Dto.Request
{
    public record AddIngredientRequest(string Name, decimal PriceForSmall, decimal PriceForMedium, decimal PriceForBig);
}