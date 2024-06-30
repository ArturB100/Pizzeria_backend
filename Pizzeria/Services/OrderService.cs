using AutoMapper;
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
        return _context.PizzaOrder.ToList();
    }

    public OperationResult AddOrder(AddOrderRequest request)
    {
        OperationResult result = new OperationResult();
        
        User? user = _context.User.FirstOrDefault(u => u.Id == request.UserId);
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
        
        List<OrderDetails> details = request.Details.Select(d => 
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

        return result;
    }
}