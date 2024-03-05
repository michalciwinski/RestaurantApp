using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using RestaurantApp.Controllers.Interface;
using RestaurantApp.Entities;
using RestaurantApp.Model;
using RestaurantApp.Services.Interface;

namespace RestaurantApp.Controllers.Implementation
{
    [Route("[controller]")]
    [ApiController]
    public class Controller_Order : ControllerBase, IController_Order
    {
        private readonly IOrderService _service;
        //private readonly RestaurantDbContext _DB;

        public Controller_Order(IOrderService orderService)
        {
            //_DB = new RestaurantDbContext();
            _service = orderService;
        }

        [HttpPost]
        [Route("AddOrder")]
        public ActionResult Post([FromBody] ModelOrder order)
        {
            var result = _service.AddOrder(order);
            return result;
            //to do
        }


    }



}
