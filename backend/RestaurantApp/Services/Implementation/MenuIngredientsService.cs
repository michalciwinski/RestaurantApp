using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenAI;
using OpenAI.Assistants;
using OpenAI.Files;
using RestaurantApp.Entities;
using RestaurantApp.Model;
using RestaurantApp.Services.Interface;
using System.IO;

namespace RestaurantApp.Services.Implementation
{
    public class MenuIngredientsService : ControllerBase, IMenuIngredientsService
    {
        private RestaurantDbContext _context;
        public MenuIngredientsService(RestaurantDbContext context)
        {
            _context = context;
        }

        public IActionResult GetDishesWithIngr()
        {
            List<ModelMenuWithIngredients> dishes = new List<ModelMenuWithIngredients>();
            try
            {
                var dishesList = _context.TvMenuIngredients.ToList();
                var dishesListGroup = dishesList.GroupBy(x => new { x.Id, x.Name });
                foreach (var group in dishesListGroup)
                {
                    var dish = new ModelMenuWithIngredients()
                    {
                        Id = group.Key.Id ?? 0,// ?? 0 to check
                        Name = group.Key.Name,
                        NameOfIngredients = group.Select(x => x.NameOfIngredient).ToList()
                    };
                    dishes.Add(dish);
                }

                return Ok(dishes);
            }
            catch (Exception ex)
            {
                return BadRequest("Can't download information from database");
            }

        }
        


    }
}
