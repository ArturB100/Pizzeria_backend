using System.Net;
using Microsoft.AspNetCore.Mvc;
using Pizzeria.Dto.Request;
using Pizzeria.Model;
using Pizzeria.Services;

namespace Pizzeria.Controllers
{
    [ApiController]
    [Route("order")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _service;
        private readonly JWTauthService _jwtauthService;
        
        public OrderController(OrderService service)
        {
            _service = service;
        }

        [HttpGet]
        public List<PizzaOrder> GetOrders()
        {
            List<PizzaOrder> orders = _service.GetOrders();
            return orders;
        }

        [HttpPost]
        [ProducesResponseType(typeof(OperationResult), (int)HttpStatusCode.OK)]
        public IActionResult AddOrder(AddOrderRequest request)
        {
            int userId = _jwtauthService.GetUserIdFromRequest(HttpContext);
            OperationResult result = _service.AddOrder(request, userId);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        
        [HttpPut("{orderId}")]
        public IActionResult ChangeOrderStatus(int orderId, OrderStatusEnum newStatus)
        {
            OperationResult result = _service.ChangeOrderStatus(orderId, newStatus);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    } 
}

