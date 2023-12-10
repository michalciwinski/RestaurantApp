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
            TOrder neworder = new TOrder
            {
                DateOfOrder = Order.DateOfOrder,
                Bill = Order.Bill,
                AdditionalComment = Order.AdditionalComment,
                TStateId = Order.StateId,
            };

            // Dodawanie zamówienia do bazy danych
             _context.TOrder.Add(neworder);
             _context.SaveChanges();
        //    List<ModelOrder> dishes = new List<ModelOrder>();

            /*    if (Order.OrderPositions != null && Order.OrderPositions.Any())
                {
                    foreach (var Position in Order.OrderPositions)
                    {
                        for (int i = 0; i < Position.Ilosc; i++)
                        {
                            TOrderPosition OrderPosition = new TOrderPosition
                            {
                                TMenuId = Position[i].TMenuId,
                                TOrderId = Position[i].OrderId, // Załóżmy, że przekazujesz również OrderId z frontu
                                                                  // Możesz dodać inne właściwości pozycji zamówienia
                            };

                            // Dodawanie pozycji zamówienia do bazy danych
                            _context.Set<TOrderPosition>().Add(pozycjaZamowienia); // Użyj Set<T>() w EF Core, jeśli nie znasz typu T podczas kompilacji
                        }
                    }
                }*/



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
