using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Pizzeria.Dto;
using Pizzeria.Model;
using Pizzeria.Services;
using System.Net;

namespace Pizzeria.Controllers
{
    [ApiController]
    [Route("pizza")]
    public class PizzaController : Controller
    {
        private readonly PizzaService _service;

        public PizzaController (PizzaService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<PizzaDto>), (int)HttpStatusCode.OK)]
        public List<PizzaDto> GetPizzas (int page)
        {
            List<PizzaDto> pizzas = _service.GetPizzas(page);
            return pizzas;
        }

        [HttpGet("{id}")]
        public IActionResult GetPizza (int id)
        {
            try
            {
                PizzaDto pizzaDto = _service.GetPizza(id); ;
                return Ok(pizzaDto);
            } catch (Exception ex)
            {
                return BadRequest();
            }

            
        }
    }
}
