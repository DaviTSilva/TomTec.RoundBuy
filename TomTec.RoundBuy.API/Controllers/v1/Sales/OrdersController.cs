using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomTec.RoundBuy.API.DTOs.v1;
using TomTec.RoundBuy.Business;
using TomTec.RoundBuy.Business.Validations;
using TomTec.RoundBuy.Data;
using TomTec.RoundBuy.Lib.AspNetCore;
using TomTec.RoundBuy.Lib.AspNetCore.Filters;
using TomTec.RoundBuy.Models;

namespace TomTec.RoundBuy.API.Controllers.v1.Sales
{
    [Route("v1/orders")]
    [ServiceFilter(typeof(KeyNotFoundExceptionFilterAttribute))]
    [ServiceFilter(typeof(UnauthorizedAccessExceptionFilterAttribute))]
    [ServiceFilter(typeof(GenericExceptionFilterAttribute))]
    public class OrdersController : Controller
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IAuthService _authService;
        private readonly IOrderService _orderService;
        public OrdersController(IRepository<Order> orderRepository, IAuthService authService, IOrderService orderService)
        {
            _orderRepository = orderRepository;
            _authService = authService;
            _orderService = orderService;
        }

        [HttpPost("")]
        public IActionResult CreateOrder([FromBody] OrderDto orderDto)
        {
            var currentUserId = _authService.GetCurrentUser(User).Id;
            var order = orderDto.ToModel(currentUserId);
            _orderService.CalculateValues(order);
            order.Validate();
            var createdOrder = _orderRepository.Create(order);
            return Created(ResponseMessage.Success, createdOrder);
        }

        [HttpGet("")]
        public IActionResult GetOrders()
        {
            return Ok(new
            {
                message = ResponseMessage.Success,
                value = _orderRepository.Get($"{nameof(Order.OrderProducts)}.{nameof(OrderProducts.Product)}")
            });
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderById(int id)
        {
            return Ok(new
            {
                message = ResponseMessage.Success,
                value = _orderRepository.Get(id, $"{nameof(Order.OrderProducts)}.{nameof(OrderProducts.Product)}")
            });
        }

        [HttpGet("buyer/{buyerUserId}")]
        public IActionResult GetOrderByBuyerUserId(int buyerUserId)
        {
            return Ok(new
            {
                message = ResponseMessage.Success,
                value = _orderRepository.Get(o => o.BuyerUserId == buyerUserId, $"{nameof(Order.OrderProducts)}.{nameof(OrderProducts.Product)}")
            });
        }
    }
}
