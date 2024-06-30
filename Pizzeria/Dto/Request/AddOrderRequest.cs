namespace Pizzeria.Dto.Request;

public record AddOrderRequest( int UserId, int AddressId, List<AddOrderDetailsRequest> Details );