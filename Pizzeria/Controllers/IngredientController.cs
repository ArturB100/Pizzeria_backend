using Microsoft.AspNetCore.Mvc;
using Pizzeria.Dto.Request;
using Pizzeria.Model;
using Pizzeria.Services;

namespace Pizzeria.Controllers;

[ApiController]
[Route("ingredient")]
public class IngredientController : ControllerBase
{
    private readonly IngredientService _service;

    public IngredientController(IngredientService service)
    {
        _service = service;
    }

    [HttpGet]
    public List<Ingredient> GetIngredients()
    {
        List<Ingredient> ingredients = _service.GetIngredients();
        return ingredients;
    }

    [HttpPost]
    public IActionResult AddIngredient(AddIngredientRequest request)
    {
        OperationResult result = _service.AddIngredient(request);
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
    public IActionResult GetIngredient(int id)
    {
        try
        {
            Ingredient ingredientDto = _service.GetIngredient(id);
            return Ok(ingredientDto);
        }
        catch (Exception ex)
        {
            return BadRequest();
        }
    }
}