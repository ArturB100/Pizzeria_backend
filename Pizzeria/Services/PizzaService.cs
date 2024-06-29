﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pizzeria.Data;
using Pizzeria.Dto;
using Pizzeria.exceptions;
using Pizzeria.Model;

namespace Pizzeria.Services
{
    public class PizzaService
    {
        private readonly PizzeriaContext _context;
        private readonly IMapper _mapper;   

        public PizzaService (PizzeriaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
            Pizza pizza = _context.Pizza.FirstOrDefault(p => p.Id == id);
            if (pizza == null) throw new NotExistingIdException();
            
            PizzaDto pizzaDto = new PizzaDto(
                pizza.Id, 
                pizza.Name, 
                pizza.CreatedByUser,
                new List<Ingredient>(),
                10,
                10,
                10
            );

            return pizzaDto;
        }
    }
}
