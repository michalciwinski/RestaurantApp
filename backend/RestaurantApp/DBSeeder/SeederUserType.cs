using RestaurantApp.Entities;

namespace RestaurantApp.DBSeeder
{
    public class SeederUserType
    {
        private RestaurantDbContext _context;
        public SeederUserType(RestaurantDbContext context)
        {
            _context = context;
        }
        private IEnumerable<TuserType> GetTUserType()
        {
            var USerTypeList = new List<TuserType>()
            {
                new TuserType()
                {
                    Type = "Admin",
                },
                new TuserType()
                {
                    Type = "User",
                },
                new TuserType()
                {
                    Type = "Guest",
                }
            };

            return USerTypeList;
        }


        public void Seeder()
        {
            if (_context.Database.CanConnect())
            {
                if (!_context.TuserTypes.Any())
                {
                    var UserType = GetTUserType();
                    _context.TuserTypes.AddRange(UserType);
                    _context.SaveChanges();
                }
            }


        }


    }
}
