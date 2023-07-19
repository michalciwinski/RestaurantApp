using Microsoft.EntityFrameworkCore;
using RestaurantApp.Entities;
using System.Xml.Linq;

namespace RestaurantApp.DBSeeder
{
    public class SeederDishType
    {
        private RestaurantDbContext _context;
        public SeederDishType(RestaurantDbContext context)
        {
            _context = context;
        }
        private IEnumerable<TDishType> GetTDishType()
        {
            var DishTypeList = new List<TDishType>()
            {
                new TDishType()
                {
                    Name = "Starter",
                },
                new TDishType()
                {
                    Name = "Soup",
                },
                new TDishType()
                {
                    Name = "Main course",
                },
                new TDishType()
                {
                    Name = "Dessert",
                },
                new TDishType()
                {
                    Name = "Drink",
                }
            };
            return DishTypeList;
        }


        public void Seeder()
        {
            if (_context.Database.CanConnect())
            {
                if (!_context.TDishType.Any())
                {
                    var DishType = GetTDishType();
                    _context.TDishType.AddRange(DishType);
                    _context.SaveChanges();
                }
            }
        }

    }
}
