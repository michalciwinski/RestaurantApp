using Microsoft.VisualStudio.Services.Common;
using RestaurantApp.Entities;


namespace RestaurantApp.DBSeeder
{
    public class SeederState
    {
        private RestaurantDbContext _context;
        public SeederState(RestaurantDbContext context)
        {
            _context = context;
        }

        private IEnumerable<Tstate> GetTState()
        {
            var StateList = new List<Tstate>()
            {
                new Tstate()
                {
                    Name = "Preparing",
                },
                new Tstate()
                {
                    Name = "Done",
                },
                new Tstate()
                {
                    Name = "Received",
                }
            };
            return StateList;
        }

        public void Seeder()
        {
            if (_context.Database.CanConnect())
            {
                if (!_context.Tstates.Any())
                {
                    var State = GetTState();
                    _context.Tstates.AddRange(State);
                    _context.SaveChanges();
                }
            }
        }

    }
}
