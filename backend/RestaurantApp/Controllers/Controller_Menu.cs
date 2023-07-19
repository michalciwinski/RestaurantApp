using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantApp.Entities;
using RestaurantApp.Model;
using RestaurantApp.Services;
using System.Reflection.Metadata.Ecma335;
using Microsoft.VisualStudio.Services.DelegatedAuthorization;

namespace RestaurantApp.Controllers
{
    
    [ApiController]
    [Route("[controller]/")]
    public class Controller_Menu : ControllerBase
    {
        private readonly MenuService _service;
        private readonly RestaurantDbContext _DB;

        public Controller_Menu()
        {
            _DB = new RestaurantDbContext();
            _service = new MenuService(_DB);
        }

        [HttpGet]
        [Route("GetDishes")]
        public IEnumerable<ModelMenu> Get() //IActionResult 
        {
            return _service.GetDishes();
        }

        [HttpGet]
        [Route("GetDish/{id}")]
        public ModelMenu Get(int id)  //IActionResult 
        {
            return _service.GetDish(id);
        }

        [HttpDelete]
        [Route("DeleteDish")] //bug - in json ID has to be written. Random nr can be there.(client site)
        public ActionResult Delete([FromBody] ModelMenu Dish)
        {
            var result = _service.DeleteDish(Dish);
            return StatusCode(result);
        }

        [HttpPost]
        [Route("AddDish")]
        public ActionResult Post([FromBody] ModelMenu Dish)
        {
            var result = _service.AddDish(Dish);
            return StatusCode(result);
            //to do
        }

        [HttpPut]
        [Route("UpdateDish")]
        public ActionResult Put([FromBody] ModelMenuToUpdate Dish)
        {
            var result = _service.UpdateDish(Dish);
            return StatusCode(result);
            //to do
        }



    }
}
