using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantApp.Entities;
using RestaurantApp.Services;

namespace RestaurantApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class Controller_Order : ControllerBase
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
