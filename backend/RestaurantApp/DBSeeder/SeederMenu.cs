using RestaurantApp.Entities;

namespace RestaurantApp.DBSeeder
{
    public class SeederMenu
    {
        private RestaurantDbContext _context;
        public SeederMenu(RestaurantDbContext context)
        {
            _context = context;
        }
        private IEnumerable<TMenu> GetTMenu()
        {
            var MenuList = new List<TMenu>()
            {
                new TMenu()
                {
                    Name = "Burger Drwala",
                    Description = "Burger wołowy z oscypkiem,  w zestawie frytki",
                    Price = 38.5,
                    TDishTypeId = 3,
                },
                new TMenu()
                {
                    Name = "Zupa pomidorowa",
                    Description = "Zupka pomidorowa na rosole z wczoraj",
                    Price = 12.5,
                    TDishTypeId = 2,
                }
            };
            return MenuList;
        }

        public void Seeder()
        {
            if (_context.Database.CanConnect())
            {
                if (!_context.TMenu.Any())
                {
                    var Menu = GetTMenu();
                    _context.TMenu.AddRange(Menu);
                    _context.SaveChanges();
                }
            }
        }

    }
}
