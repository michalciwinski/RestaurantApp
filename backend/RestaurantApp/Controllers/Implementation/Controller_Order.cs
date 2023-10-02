using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using RestaurantApp.Controllers.Interface;
using RestaurantApp.Entities;
using RestaurantApp.Services;

namespace RestaurantApp.Controllers.Implementation
{
    [Route("[controller]")]
    [ApiController]
    public class Controller_Order : ControllerBase, IController_Order
    {
        private readonly OrderService _service;
        private readonly RestaurantDbContext _DB;

        public Controller_Order()
        {
            _DB = new RestaurantDbContext();
            _service = new OrderService(_DB);
        }

    }



}
