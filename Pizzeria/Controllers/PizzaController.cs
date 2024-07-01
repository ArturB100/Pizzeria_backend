using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Pizzeria.Dto;
using Pizzeria.Dto.Request;
using Pizzeria.Model;
using Pizzeria.Services;
using System.Net;
using Pizzeria.Dto.Request;

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
        [ProducesResponseType(typeof(List<PizzaDto>), (int)HttpStatusCode.OK)]
        public List<PizzaDto> GetPizzas (int page)
        {
            List<PizzaDto> pizzas = _service.GetPizzas(page);
            return pizzas;
        }
        
        [HttpPost]
        public int AddPizza (AddPizzaRequest request)
        {
            return _service.AddPizza(request);
        }
        
        [HttpPost("assign-image-to-pizza/{pizzaId}")]
        public void AssignImageToPizza (int pizzaId, IFormFile image)
        {
            _service.AddImage(pizzaId, image);
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
