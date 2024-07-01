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
        private readonly IWebHostEnvironment _hostingEnvironment;

       

        public PizzaController (PizzaService service, IWebHostEnvironment hostingEnvironment)
        {
            _service = service;
            _hostingEnvironment = hostingEnvironment;

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
        [HttpPost("upload")]
        async public Task<IActionResult> AssignImageToPizza (IFormFile file)
        {
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images");

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            var filePath2 = Path.Combine(filePath, uniqueFileName);

            using (var stream = new FileStream(filePath2, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return Ok("dsada");


        }
    }
}
