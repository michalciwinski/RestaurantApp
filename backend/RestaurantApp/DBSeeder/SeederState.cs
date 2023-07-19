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

        private IEnumerable<TState> GetTState()
        {
            var StateList = new List<TState>()
            {
                new TState()
                {
                    Name = "Preparing",
                },
                new TState()
                {
                    Name = "Done",
                },
                new TState()
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
                if (!_context.TState.Any())
                {
                    var State = GetTState();
                    _context.TState.AddRange(State);
                    _context.SaveChanges();
                }
            }
        }

    }
}
