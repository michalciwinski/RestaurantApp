using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantApp.Entities;
using RestaurantApp.Model;
using System.Reflection.Metadata.Ecma335;
using Microsoft.VisualStudio.Services.DelegatedAuthorization;
using RestaurantApp.Controllers.Interface;
using RestaurantApp.Services.Implementation;
using RestaurantApp.Services.Interface;
using Microsoft.AspNetCore.Authorization;

namespace RestaurantApp.Controllers.Implementation
{

    [ApiController]
    [Route("api/Menu")]
    public class Controller_Menu : ControllerBase, IController_Menu
    {
        private readonly IMenuService _service;

        public Controller_Menu(IMenuService menuService)
        {
            _service = menuService;
        }

        [HttpGet]
        [Route("GetDishes")]
        public IActionResult Get()
        {
            return _service.GetDishes();
        }

        [HttpGet]
        [Route("GetDish/{id}")]
        public IActionResult Get(int id)
        {
            return _service.GetDish(id);
        }

        [HttpGet]
        [Route("GetIngredientsOfDish/{id}")]
        public IActionResult GetIngredientsOfDish(int id)
        {
            return _service.GetIngredientsOfDish(id);
        }

        [HttpDelete]
        [Route("DeleteDish")] 
        [Authorize(Roles = "Admin")]//onlyAdmin
        public IActionResult Delete(int id)
        {
            return _service.DeleteDish(id);
        }

        [HttpPost]
        [Route("AddDish")]
        [Authorize(Roles = "Admin")]//onlyAdmin
        public IActionResult Post([FromForm] ModelMenuWithPicture modelMenuWithPicture,[FromForm] ModelListOfIngredients modelListOfIngredients)
        {
            return _service.AddDish(modelMenuWithPicture, modelListOfIngredients);
        }

        [HttpPut]
        [Route("UpdateDish")]
        [Authorize(Roles = "Admin")]//onlyAdmin
        public IActionResult Put([FromBody] ModelMenuToUpdate Dish)
        {
            return _service.UpdateDish(Dish);
        }



    }
}
