using Pizzeria.Data;
using Pizzeria.Dto.Request;
using Pizzeria.exceptions;
using Pizzeria.Model;

namespace Pizzeria.Services;

public class IngredientService
{
    private readonly PizzeriaContext _context;

    public IngredientService(PizzeriaContext context)
    {
        _context = context;
    }

    public List<Ingredient> GetIngredients()
    {
        return _context.Ingredient.ToList();
    }

    public OperationResult AddIngredient(AddIngredientRequest request)
    {
        Ingredient ingredient = new Ingredient
        {
            Name = request.Name,
            PriceForSmall = request.PriceForSmall,
            PriceForMedium = request.PriceForMedium,
            PriceForBig = request.PriceForBig
        };

        _context.Ingredient.Add(ingredient);
        _context.SaveChanges();

        return new OperationResult { Success = true };
    }

    public Ingredient GetIngredient(int id)
    {
        Ingredient ingredient = _context.Ingredient.FirstOrDefault(i => i.Id == id);
        if (ingredient == null) throw new NotExistingIdException();

        return ingredient;
    }
}