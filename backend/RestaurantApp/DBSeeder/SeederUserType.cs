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
        private IEnumerable<TUserType> GetTUserType()
        {
            var USerTypeList = new List<TUserType>()
            {
                new TUserType()
                {
                    Type = "Admin",
                },
                new TUserType()
                {
                    Type = "User",
                },
                new TUserType()
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
                if (!_context.TUserType.Any())
                {
                    var UserType = GetTUserType();
                    _context.TUserType.AddRange(UserType);
                    _context.SaveChanges();
                }
            }


        }


    }
}
