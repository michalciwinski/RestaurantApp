using Microsoft.EntityFrameworkCore;
using RestaurantApp.Entities;

namespace RestaurantApp.DBSeeder
{
    public class SeederDishType
    {
        private RestaurantDbContext _context;
        public SeederDishType(RestaurantDbContext context)
        {
            _context = context;
        }
        private IEnumerable<TdishType> GetTDishType()
        {
            var DishTypeList = new List<TdishType>()
            {
                new TdishType()
                {
                    Name = "Starter",
                },
                new TdishType()
                {
                    Name = "Soup",
                },
                new TdishType()
                {
                    Name = "Main course",
                },
                new TdishType()
                {
                    Name = "Dessert",
                },
                new TdishType()
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
                if (!_context.TdishTypes.Any())
                {
                    var DishType = GetTDishType();
                    _context.TdishTypes.AddRange(DishType);
                    _context.SaveChanges();
                }
            }
        }

    }
}
