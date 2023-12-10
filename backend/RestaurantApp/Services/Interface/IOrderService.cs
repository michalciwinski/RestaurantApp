using Microsoft.AspNetCore.Mvc;
using RestaurantApp.Model;

namespace RestaurantApp.Services.Interface
{
    public interface IOrderService
    {
        ActionResult AddOrder(ModelOrder Order);
    }
}
