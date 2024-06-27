using Microsoft.EntityFrameworkCore;
using Pizzeria.Data;
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
    }
}
