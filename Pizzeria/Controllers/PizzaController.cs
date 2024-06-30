using Microsoft.AspNetCore.Mvc;
using Pizzeria.Dto.Request;
using Pizzeria.Model;
using Pizzeria.Services;

namespace Pizzeria.Controllers
{
    [ApiController]
    [Route("pizza")]
    public class PizzaController : ControllerBase
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
        
        [HttpPost]
        public IActionResult AddPizza (AddPizzaRequest request)
        {
            OperationResult result = _service.AddPizza(request);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
