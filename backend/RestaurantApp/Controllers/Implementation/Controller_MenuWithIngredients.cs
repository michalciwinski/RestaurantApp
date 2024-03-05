using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using RestaurantApp.Controllers.Interface;
using RestaurantApp.Entities;
using RestaurantApp.Model;
using RestaurantApp.Services.Interface;

namespace RestaurantApp.Controllers.Implementation
{
    [ApiController]
    [Route("[controller]/")]
    public class Controller_MenuWithIngredients : Controller, IController_MenuWithIngredients
    {

        private readonly IMenuIngredientsService _service;
        public Controller_MenuWithIngredients(IMenuIngredientsService menuIngredientsService)
        {
            _service = menuIngredientsService;
        }

        [HttpGet]
        [Route("GetDishesWithIngredients")]

        public IActionResult Get()
        {
            return _service.GetDishesWithIngr();
        }

        

    }
}
