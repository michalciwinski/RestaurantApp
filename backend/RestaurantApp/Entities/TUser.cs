using System.ComponentModel.DataAnnotations;

namespace RestaurantApp.Entities
{
    public class TUser
    {
        [Key, Required]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string QR { get;set; }
        public string Password { get; set; }
        //RELATION
        public virtual TUserType TUserType { get; set; } 
        public int TUserTypeId { get; set; }
        public virtual List<TOrder> TOrder { get; set; }
        

    }
}
