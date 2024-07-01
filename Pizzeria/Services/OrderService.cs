using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pizzeria.Config;
using Pizzeria.Data;
using Pizzeria.Dto;
using Pizzeria.Dto.Request;
using Pizzeria.Model;

namespace Pizzeria.Services;

public class OrderService
{
    private readonly PizzeriaContext _context;
    private readonly IMapper _mapper;

    public OrderService(PizzeriaContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public List<PizzaOrder> GetOrders()
    {
        return _context.PizzaOrder
            .Include(p => p.User)
            .Include(p => p.Address)
            .ToList();
    }


    public List<PizzaOrder> GetOrdersOfUser(int userId)
    {
        return _context.PizzaOrder            
            .Include(p => p.User)
            .Include(p => p.Address)
            .Include(p => p.OrderDetails)
                .ThenInclude(o => o.Pizza)            
            .Where(p => p.User.Id == userId)
            .ToList();
    }



    public OperationResult AddOrder(AddOrderRequest request, int userId)
    {
        OperationResult result = new OperationResult();
        
        User? user = _context.User.FirstOrDefault(u => u.Id == userId);
        if (user == null)
        {
            result.Success = false;
            result.Errors.Add(new FieldError() { FieldKey = "userId", ErrorMsg = "Nie ma takiego uzytkownika" });
        }
        
        Address? address = _context.Address.FirstOrDefault(a => a.Id == request.AddressId);
        if(address == null)
        {
            result.Success = false;
            result.Errors.Add(new FieldError() { FieldKey = "addressId", ErrorMsg = "Nie ma takiego adresu" });
        }
        
        List<OrderDetails> orderDetails = request.Details.Select(d => 
        {
            var pizza = _context.Pizza.SingleOrDefault(p => p.Id == d.PizzaId);
            if (pizza == null) 
            {
                throw new Exception($"Pizza with id {d.PizzaId} does not exist.");
            }
            
            SizeEnum size = (SizeEnum)d.Size;
            
            return new OrderDetails()
            {
                Pizza = pizza,
                Size = size,
                Quantity = d.Quantity
            };
        }).ToList();

        PizzaOrder pizza = new PizzaOrder {
            Address = address,
            User = user,
            Status = OrderStatusEnum.InPreparation,
            OrderDetails = orderDetails,
            TotalPrice = orderDetails.Sum(od =>
            {
                decimal costOfIngredients = od.Pizza.Ingredients.Sum(i =>
                {
                    if (od.Size == SizeEnum.Small)
                    {
                        return i.PriceForSmall;
                    }
                    else if (od.Size == SizeEnum.Medium) 
                    {
                        return i.PriceForMedium;
                    }
                    else if (od.Size == SizeEnum.Large)
                    {
                        return i.PriceForBig;
                    }
                    return i.PriceForMedium;
                });

                return (double)(costOfIngredients * od.Quantity);

            })
        };

        result = MyUtils.ValidateModel(pizza);
        if (!result.Success)
        {
            return result;
        }
        _context.PizzaOrder.Add(pizza);
        _context.SaveChanges();

        return result;
    }
    
    public OperationResult ChangeOrderStatus(int orderId, OrderStatusEnum newStatus)
    {
        OperationResult result = new OperationResult();

        PizzaOrder? order = _context.PizzaOrder.FirstOrDefault(o => o.Id == orderId);
        if (order == null)
        {
            result.Success = false;
            result.Errors.Add(new FieldError() { FieldKey = "orderId", ErrorMsg = "Nie ma takiego zamówienia" });
            return result;
        }

        order.Status = newStatus;
        _context.SaveChanges();

        return result;
    }
}