using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantApp.Entities;
using RestaurantApp.Model;
using RestaurantApp.Services.Interface;



namespace RestaurantApp.Services.Implementation
{
    

    public class OrderService : ControllerBase, IOrderService
    {
        private RestaurantDbContext _context;
        public OrderService(RestaurantDbContext context)
        {
            _context = context;
        }

        public ActionResult AddOrder(ModelOrder Order)
        {
            if (Order == null)
            {
                return BadRequest();
            }
            Torder neworder = new Torder
            {
                DateOfOrder = Order.DateOfOrder,
                Bill = Order.Bill,
                AdditionalComment = Order.AdditionalComment,
                TstateId = Order.StateId,
            };

             _context.Torders.Add(neworder);
             _context.SaveChanges();

            try
            {
               // _context.TUser.Add(newUser);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("error with adding order to database");
            }
        }

    }
}
