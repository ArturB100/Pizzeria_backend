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
