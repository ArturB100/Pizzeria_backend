using Microsoft.AspNetCore.Mvc;
using Pizzeria.Model;
using Pizzeria.Services;

namespace Pizzeria.Controllers
{
    [ApiController]
    [Route("pizza")]
    public class PizzaController
    {
        private readonly PizzaService _service;

        public PizzaController (PizzaService service)
        {
            _service = service;
        }

        [HttpGet]
        public List<Pizza> GetPizzas (int page)
        {
            List<Pizza> pizzas = _service.GetPizzas(page);
            return pizzas;
        }
    }
}
