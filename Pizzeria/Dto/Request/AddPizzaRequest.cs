namespace Pizzeria.Dto.Request
{
    public record AddPizzaRequest(string Name, List<int> Ingredients, IFormFile Image);
}