namespace Pizzeria.Dto.Request;

public record AddOrderRequest( int AddressId, List<AddOrderDetailsRequest> Details );