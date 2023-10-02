using RestaurantApp.Model;

namespace RestaurantApp.Services.Interface
{
    public interface IMenuService
    {
        List<ModelMenu> GetDishes();
        ModelMenu GetDish(int id);
        int DeleteDish(ModelMenu Dish);
        int AddDish(ModelMenu Dish);
        int UpdateDish(ModelMenuToUpdate Dish);
    }
}
