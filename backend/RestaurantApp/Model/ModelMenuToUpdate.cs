namespace RestaurantApp.Model
{
    public class ModelMenuToUpdate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string NewName { get; set; }
        public string NewDescription { get; set; }
        public double NewPrice { get; set; }
    }
}
