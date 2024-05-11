namespace RestaurantApp.Model
{
    public class ModelMenuToUpdate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int DishType { get; set; }
        public Dictionary<int,string> Ingredients { get; set; }
    }
}
