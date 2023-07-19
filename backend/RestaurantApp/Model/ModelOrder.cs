namespace RestaurantApp.Model
{
    public class ModelOrder
    {
        public DateTime DateOfOrder { get; set; }
        public int Bill { get; set; }
        public string AdditionalComment { get; set; }
        public string State { get; set; }
    }
}
