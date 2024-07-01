using Microsoft.AspNetCore.Mvc;
using Pizzeria.Dto.Request;
using Pizzeria.Model;
using Pizzeria.Services;
using System.Net;

namespace Pizzeria.Controllers
{
    [ApiController]
    [Route("order")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _service;
        private readonly JWTauthService _jwtauthService;

        public OrderController(OrderService service, JWTauthService jWTauthService)
        {
            _service = service;
            _jwtauthService = jWTauthService;
        }

        [HttpGet]
        public List<PizzaOrder> GetOrders()
        {
            List<PizzaOrder> orders = _service.GetOrders();
            return orders;
        }

        [HttpGet("user")]
        [ProducesResponseType(typeof(List<PizzaOrder>), (int)HttpStatusCode.OK)]
        public IActionResult GetOrdersOfCurrentLoggedUser ()
        {
            int userId = _jwtauthService.GetUserIdFromRequest(HttpContext);
            if (userId == 0)
            {
                return Unauthorized();
            }
            var list = _service.GetOrdersOfUser(userId);
            return Ok(list);
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

