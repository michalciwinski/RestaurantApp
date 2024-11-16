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

        public IActionResult AddOrder(ModelOrder modelOrder)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var newOrder = new Torder
                    {
                        DateOfOrder = modelOrder.DateOfOrder,
                        Bill = modelOrder.Bill,
                        AdditionalComment = modelOrder.AdditionalComment,
                        TuserId = 1, // TO DO oter ID of User if he/she is logged in
                        TstateId = modelOrder.StateId
                    };

                    _context.Torders.Add(newOrder);
                    _context.SaveChanges();

                    foreach (var modelOrderPosition in modelOrder.OrderPositions)
                    {
                        var newOrderPosition = new TorderPosition
                        {
                            TorderId = newOrder.Id, //id of new order
                            TmenuId = modelOrderPosition.TMenuId
                        };

                        _context.TorderPositions.Add(newOrderPosition);
                    }

                    _context.SaveChanges();
                    transaction.Commit();
                    return new OkObjectResult(new { message = "Order added successfully" });
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return new StatusCodeResult(StatusCodes.Status500InternalServerError);
                }
            }
            
        }

    }
}
