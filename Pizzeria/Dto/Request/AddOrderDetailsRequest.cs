namespace Pizzeria.Dto.Request;

public record AddOrderDetailsRequest( int PizzaId, int Quantity, int Size );