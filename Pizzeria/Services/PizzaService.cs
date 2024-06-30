using Microsoft.EntityFrameworkCore;
using Pizzeria.Data;
using Pizzeria.Dto.Request;
using Pizzeria.Model;

namespace Pizzeria.Services
{
    public class PizzaService
    {
        private readonly PizzeriaContext _context;

        public PizzaService (PizzeriaContext context)
        {
            _context = context;
        }

        public List<Pizza> GetPizzas (int page)
        {
            return _context.Pizza
                .Include(p => p.Ingredients) 
                .Skip (page * 10).Take(10).ToList ();
        }

        public OperationResult AddPizza(AddPizzaRequest request)
        {
            List<Ingredient> ingredients = _context.Ingredient.Where(i => request.Ingredients.Contains(i.Id)).ToList();
            if (ingredients.Count != request.Ingredients.Count)
            {
                return new OperationResult { Success = false };
            }
            
            Pizza pizza = new Pizza { Name = request.Name, Ingredients = ingredients };
            _context.Pizza.Add(pizza);
            _context.SaveChanges();
            return new OperationResult { Success = true };
        }
    }
}
