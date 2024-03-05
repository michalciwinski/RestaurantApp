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
        private IEnumerable<Tmenu> GetTMenu()
        {
            var MenuList = new List<Tmenu>()
            {
                new Tmenu()
                {
                    Name = "Burger Drwala",
                    Description = "Burger wołowy z oscypkiem,  w zestawie frytki",
                    Price = 38.5,
                    TdishTypeId = 3,
                },
                new Tmenu()
                {
                    Name = "Zupa pomidorowa",
                    Description = "Zupka pomidorowa na rosole z wczoraj",
                    Price = 12.5,
                    TdishTypeId = 2,
                }
            };
            return MenuList;
        }

        public void Seeder()
        {
            if (_context.Database.CanConnect())
            {
                if (!_context.Tmenus.Any())
                {
                    var Menu = GetTMenu();
                    _context.Tmenus.AddRange(Menu);
                    _context.SaveChanges();
                }
            }
        }

    }
}
