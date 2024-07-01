using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pizzeria.Data;
using Pizzeria.Dto.Request;
using Pizzeria.Dto;
using Pizzeria.exceptions;
using Pizzeria.Model;

namespace Pizzeria.Services
{
    public class PizzaService
    {
        private readonly PizzeriaContext _context;
        private readonly IMapper _mapper;   
        private readonly IWebHostEnvironment _hostingEnvironment;

        public PizzaService (PizzeriaContext context, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }

        public List<PizzaDto> GetPizzas (int page)
        {
            return _context.Pizza
                .Include(p => p.Ingredients) 
                .Skip (page * 10).Take(10)
                .Select(p => new PizzaDto(
                    p.Id,
                    p.Name,
                    p.CreatedByUser, 
                    p.Ingredients,
                    p.Ingredients.Sum(i => i.PriceForSmall),
                    p.Ingredients.Sum(i => i.PriceForMedium),
                    p.Ingredients.Sum(i => i.PriceForBig)
                    ))
                .ToList ();
        }

        public PizzaDto GetPizza (int id)
        {
            Pizza pizza = _context.Pizza
                .Include(p => p.Ingredients)
                .FirstOrDefault(p => p.Id == id);

            if (pizza == null) throw new NotExistingIdException();
            
            PizzaDto pizzaDto = new PizzaDto(
                pizza.Id, 
                pizza.Name, 
                pizza.CreatedByUser,
                pizza.Ingredients,
                10,
                10,
                10
            );

            return pizzaDto;
        }

        public int AddPizza(AddPizzaRequest request)
        {
            List<Ingredient> ingredients = _context.Ingredient.Where(i => request.Ingredients.Contains(i.Id)).ToList();
            if (ingredients.Count != request.Ingredients.Count)
            {
                return -1;
            }

            Pizza pizza = new Pizza { Name = request.Name, Ingredients = ingredients };
            _context.Pizza.Add(pizza);
            _context.SaveChanges();

            return pizza.Id;
        }

        async public void AddImage(int pizzaId, IFormFile image)
        {
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images");

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            
            var uniqueFileName = pizzaId.ToString() + ".jpg";
            var filePath2 = Path.Combine(filePath, uniqueFileName);

            using (var stream = new FileStream(filePath2, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }
        }
    }
}
