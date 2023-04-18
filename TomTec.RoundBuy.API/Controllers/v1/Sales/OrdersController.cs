using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomTec.RoundBuy.API.DTOs.v1;
using TomTec.RoundBuy.Business;
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

        public OrdersController(IRepository<Order> orderRepository, IAuthService authService)
        {
            _orderRepository = orderRepository;
            _authService = authService;
        }

        [HttpPost("")]
        public IActionResult CreateOrder([FromBody] OrderDto orderDto)
        {
            var currentUserId = _authService.GetCurrentUser(User).Id;
            var order = _orderRepository.Create(orderDto.ToModel(currentUserId));

            return Created(ResponseMessage.Success, order);
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
