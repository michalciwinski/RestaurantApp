using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using RestaurantApp.Controllers.Interface;
using RestaurantApp.Entities;
using RestaurantApp.Model;
using RestaurantApp.Services.Interface;

namespace RestaurantApp.Controllers.Implementation
{
    [Route("api/Order")]
    [ApiController]
    public class Controller_Order : ControllerBase, IController_Order
    {
        private readonly IOrderService _service;

        public Controller_Order(IOrderService orderService)
        {
            _service = orderService;
        }

        [HttpPost]
        [Route("AddOrder")]
        public IActionResult Post([FromBody] ModelOrder order)
        {
            var result = _service.AddOrder(order);
            return result;
        }


    }



}
